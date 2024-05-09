// <copyright file="ManageMartialBaseUserDialogBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Blazored.Modal;
using Blazored.Modal.Services;

using MartialBase.API.Models.DTOs.MartialBaseUsers;
using MartialBase.API.Models.DTOs.UserRoles;
using MartialBase.Web.App.Components.Common;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

using Microsoft.AspNetCore.Components;

namespace MartialBase.Web.App.Components.MartialBaseUsers
{
    public class ManageMartialBaseUserDialogBase : ComponentBase
    {
        public MartialBaseUserDTO MartialBaseUser { get; set; }

        [Inject]
        public IMartialBaseUserDataService MartialBaseUserDataService { get; set; }

        [Parameter]
        public string MartialBaseUserId { get; set; }

        [Inject]
        public IAuthDataService AuthDataService { get; set; }

        [Inject]
        public IAuthTokensService AuthTokensService { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }

        public string ConfirmationMessage { get; set; }

        public bool ConfirmationMessageVisible { get; set; }

        public string ErrorMessage { get; set; }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public bool IsLoading => !string.IsNullOrEmpty(LoadingMessage);

        public string LoadingMessage { get; set; }

        [Inject]
        public ILocalizerService Localizer { get; set; }

        [Inject]
        public IModalService ModalService { get; set; }

        public string StatusClass { get; set; }

        public string UserName { get; set; }

        public List<CheckBoxItem<UserRoleDTO>> UserRoles { get; set; }

        public void HideError()
        {
            ErrorMessage = null;
            StateHasChanged();
        }

        public async Task GetInvitationCode()
        {
            LoadingMessage = Localizer["GettingInvitationCode"];
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            ApiResult<string> getInvitationCodeResult =
                await MartialBaseUserDataService.GetInvitationCode(MartialBaseUserId, authToken);

            if (!getInvitationCodeResult.IsSuccess)
            {
                ShowConfirmationMessage(
                    $"{Localizer["FailedToGetInvitationCodeForUser"]} {Localizer[getInvitationCodeResult.ErrorResponseCode.ToString()]}",
                    true);
            }
            else
            {
                ShowConfirmationMessage(string.Format(Localizer["SuccessfullyRetrievedInvitationCode"], getInvitationCodeResult.Object));
            }

            LoadingMessage = null;
        }

        public async Task LockOutUser()
        {
            var parameters = new ModalParameters();
            parameters.Add("ConfirmationMessage", $"Are you sure you wish to lock out '{UserName}'?");
            var confirmLockOut = ModalService.Show<ConfirmationYesNo>(Localizer["ConfirmLockOutTitle"], parameters);
            var result = await confirmLockOut.Result;

            if (result.Cancelled)
            {
                return;
            }

            LoadingMessage = $"Locking out user '{UserName}'...";
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            ApiResult lockOutResult = await AuthDataService.LockOutUser(MartialBaseUserId, authToken);

            if (lockOutResult.IsSuccess)
            {
                LoadingMessage = null;
                await LoadUser();

                ShowConfirmationMessage($"User {UserName} has been locked out.", true);
            }
            else
            {
                LoadingMessage = null;
                ErrorMessage = $"Failed to lock out user. {Localizer[lockOutResult.ErrorResponseCode.ToString()]}";
            }
        }

        public async Task LoadUser()
        {
            LoadingMessage = Localizer["LoadingUserDetails"];
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            ApiResult<MartialBaseUserDTO> getUserResult =
                await MartialBaseUserDataService.GetUser(MartialBaseUserId, authToken, true);

            if (getUserResult.IsSuccess)
            {
                MartialBaseUser = getUserResult.Object;
                UserName = MartialBaseUser.Person != null
                    ? $"{MartialBaseUser.Person.FirstName} {MartialBaseUser.Person.LastName} ({MartialBaseUser.Person.Email})"
                    : MartialBaseUser.Person.Email;

                LoadingMessage = Localizer["LoadingUserRoles"];
                StateHasChanged();

                ApiResult<List<UserRoleDTO>> getUserRolesResult =
                    await MartialBaseUserDataService.GetRolesForUser(MartialBaseUser.Id, authToken);

                if (getUserRolesResult.IsSuccess)
                {
                    foreach (CheckBoxItem<UserRoleDTO> roleCheckItem in UserRoles)
                    {
                        if (getUserRolesResult.Object.Any(ur => ur.Id == roleCheckItem.Id))
                        {
                            roleCheckItem.Checked = true;
                        }
                    }

                    LoadingMessage = null;
                }
                else
                {
                    ErrorMessage =
                        $"{Localizer["FailedToLoadUserRoles"]} {Localizer[getUserRolesResult.ErrorResponseCode.ToString()]}";
                    LoadingMessage = null;
                }
            }
            else
            {
                ErrorMessage =
                    $"{Localizer["FailedToLoadUserDetails"]} {Localizer[getUserResult.ErrorResponseCode.ToString()]}";
                LoadingMessage = null;
            }
        }

        public async Task SaveUserRoles()
        {
            LoadingMessage = Localizer["SavingUserRoles"];
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            var userRoles = new List<Guid>();

            foreach (var role in UserRoles)
            {
                if (role.Checked)
                {
                    userRoles.Add(new Guid(role.Id));
                }
            }

            ApiResult setRolesResult =
                await MartialBaseUserDataService.SetUserRoles(MartialBaseUserId, userRoles, authToken);

            if (!setRolesResult.IsSuccess)
            {
                ShowConfirmationMessage(
                    $"{Localizer["FailedToSetRolesForUser"]} {Localizer[setRolesResult.ErrorResponseCode.ToString()]}",
                    true);
            }
            else
            {
                ShowConfirmationMessage(Localizer["UserRolesUpdatedSuccessfully"]);
            }

            LoadingMessage = null;
        }

        public async Task Cancel()
        {
            await BlazoredModal.Close(ModalResult.Cancel());
        }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            HideConfirmationMessage();
            LoadingMessage = Localizer["LoadingSystemRoles"];
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            MartialBaseUser = new MartialBaseUserDTO();
            UserRoles = new List<CheckBoxItem<UserRoleDTO>>();

            ApiResult<List<UserRoleDTO>> getRolesResult = await MartialBaseUserDataService.GetRoles(authToken);

            if (getRolesResult.IsSuccess)
            {
                foreach (UserRoleDTO role in getRolesResult.Object)
                {
                    UserRoles.Add(new CheckBoxItem<UserRoleDTO>
                        { Id = role.Id, DisplayValue = role.Name, Checked = false, Object = role });
                }
            }
            else
            {
                ErrorMessage =
                    $"{Localizer["FailedToLoadSystemRoles"]} {Localizer[getRolesResult.ErrorResponseCode.ToString()]}";
                LoadingMessage = null;
            }

            await LoadUser();
        }

        private void ShowConfirmationMessage(string confirmationMessage, bool isWarning = false)
        {
            ConfirmationMessage = confirmationMessage;
            StatusClass = !isWarning ? "alert-success" : "alert-danger";
            ConfirmationMessageVisible = true;
            StateHasChanged();
        }

        private void HideConfirmationMessage()
        {
            ConfirmationMessage = string.Empty;
            ConfirmationMessageVisible = false;
            StateHasChanged();
        }
    }
}