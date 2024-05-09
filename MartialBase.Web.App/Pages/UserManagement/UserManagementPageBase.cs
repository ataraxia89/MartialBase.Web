// <copyright file="UserManagementPageBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Blazored.Modal;
using Blazored.Modal.Services;

using MartialBase.API.Models.DTOs.MartialBaseUsers;
using MartialBase.API.Models.DTOs.People;
using MartialBase.Web.App.Components.Common;
using MartialBase.Web.App.Components.MartialBaseUsers;
using MartialBase.Web.App.Components.People;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace MartialBase.Web.App.Pages.UserManagement
{
    public class UserManagementPageBase : MartialBasePageBase
    {
        [Inject]
        public IAuthTokensService AuthTokensService { get; set; }

        public Guid CurrentPersonId { get; set; }

        public string CurrentUserId { get; set; }

        [Inject]
        public IMartialBaseUserDataService MartialBaseUserDataService { get; set; }

        [BindProperty]
        public List<MartialBaseUserDTO> MartialBaseUsers { get; set; }

        [Inject]
        public IModalService ModalService { get; set; }

        [Inject]
        public IPeopleDataService PeopleDataService { get; set; }

        public async Task ShowAddPersonDialog(string userId)
        {
            HideConfirmationMessage();

            var parameters = new ModalParameters();
            parameters.Add(
                "ConfirmationMessage",
                Localizer["CreateNewPersonOrFindExistingPrompt"]);
            parameters.Add("Option1Text", Localizer["AddNewPersonOption"]);
            parameters.Add("Option2Text", Localizer["FindExistingPersonOption"]);
            var choiceModal = ModalService.Show<ChoiceTwoOptions>(Localizer["AddNewRecordTitle"], parameters);
            var result = await choiceModal.Result;

            if (!result.Cancelled)
            {
                if ((int)result.Data == 1)
                {
                    parameters = new ModalParameters();
                    parameters.Add("MartialBaseUserId", userId);
                    var addPersonModal = ModalService.Show<AddPersonDialog>(Localizer["AddPersonDetails"], parameters);
                    result = await addPersonModal.Result;

                    if (!result.Cancelled)
                    {
                        await AssignPersonToUser(userId, ((PersonDTO)result.Data).Id);
                    }
                }
                else
                {
                    var findPersonModal =
                        ModalService.Show<FindPersonDialog>(Localizer["FindPersonTitle"]);
                    result = await findPersonModal.Result;

                    if (!result.Cancelled)
                    {
                        await AssignPersonToUser(userId, (Guid)result.Data);
                    }
                }
            }
        }

        public async Task ShowManageUserDialog(string userId, string userName)
        {
            HideConfirmationMessage();

            var parameters = new ModalParameters();
            parameters.Add("MartialBaseUserId", userId);
            var manageUserModal =
                ModalService.Show<ManageMartialBaseUserDialog>($"{Localizer["ManageUserTitle"]} - {userName}", parameters);
            var result = await manageUserModal.Result;

            if (!result.Cancelled)
            {
                await LoadUsers();
            }
        }

        public async Task ShowEditPersonDialog(string userId, Guid? personId)
        {
            if (personId != null)
            {
                HideConfirmationMessage();

                var parameters = new ModalParameters();
                parameters.Add(
                    "ConfirmationMessage",
                    Localizer["EditOrChangeAssignedPersonPrompt"]);
                parameters.Add("Option1Text", Localizer["EditAssignedRecordOption"]);
                parameters.Add("Option2Text", Localizer["ChangeAssignedPersonOption"]);
                var choiceModal = ModalService.Show<ChoiceTwoOptions>(Localizer["EditExistingRecordPrompt"], parameters);
                var result = await choiceModal.Result;

                if (!result.Cancelled)
                {
                    if ((int)result.Data == 1)
                    {
                        parameters = new ModalParameters();
                        parameters.Add("PersonId", (Guid)personId);
                        var editPersonModal =
                            ModalService.Show<EditPersonDialog>(Localizer["EditPersonDetailsTitle"], parameters);
                        result = await editPersonModal.Result;

                        if (!result.Cancelled)
                        {
                            var updatedPerson = (PersonDTO)result.Data;

                            ShowConfirmationMessage(
                                string.Format(Localizer["DetailsForPersonUpdatedSuccessfully"], $"{updatedPerson.FirstName} {updatedPerson.LastName}"));

                            await LoadUsers();
                        }
                    }
                    else
                    {
                        var findPersonModal =
                            ModalService.Show<FindPersonDialog>(Localizer["FindPersonTitle"]);
                        result = await findPersonModal.Result;

                        if (!result.Cancelled)
                        {
                            await AssignPersonToUser(userId, (Guid)result.Data);
                        }
                    }
                }
            }
        }

        public async Task AssignPersonToUser(string userId, Guid personId)
        {
            LoadingMessage = Localizer["AssigningPersonToUser"];
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            string personName = string.Empty;

            ApiResult<PersonDTO> getPersonResult = await PeopleDataService.GetPerson(personId, authToken);

            if (getPersonResult.IsSuccess)
            {
                personName = $"{getPersonResult.Object.FirstName} {getPersonResult.Object.LastName}";
            }

            ApiResult putPersonIdResult =
                await MartialBaseUserDataService.AssignPersonToUser(userId, personId, authToken);

            if (putPersonIdResult.IsSuccess)
            {
                ShowConfirmationMessage(
                    string.Format(Localizer["PersonAddedToUser"], personName));

                await LoadUsers();
            }
            else
            {
                ErrorMessage =
                    $"{Localizer["FailedToAssignPersonToUser"]} {Localizer[putPersonIdResult.ErrorResponseCode.ToString()]}";
                LoadingMessage = null;
            }

            StateHasChanged();
        }

        public async Task LoadUsers()
        {
            CurrentUserId = null;
            CurrentPersonId = default;
            LoadingMessage = Localizer["LoadingUsers"];
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            ApiResult<List<MartialBaseUserDTO>> getUsersResult = await MartialBaseUserDataService.GetUsers(authToken);

            if (getUsersResult.IsSuccess)
            {
                MartialBaseUsers = getUsersResult.Object;
                LoadingMessage = null;
            }
            else
            {
                ErrorMessage = $"{Localizer["FailedToLoadUsers"]} {Localizer[getUsersResult.ErrorResponseCode.ToString()]}";
                LoadingMessage = null;
            }
        }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            HideConfirmationMessage();
            await LoadUsers();
        }
    }
}