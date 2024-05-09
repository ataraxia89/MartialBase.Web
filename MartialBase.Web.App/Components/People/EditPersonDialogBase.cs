// <copyright file="EditPersonDialogBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using MartialBase.API.Models.DTOs.Addresses;
using MartialBase.API.Models.DTOs.Countries;
using MartialBase.API.Models.DTOs.People;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace MartialBase.Web.App.Components.People
{
    public class EditPersonDialogBase : ComponentBase
    {
        [Inject]
        public IAuthTokensService AuthTokensService { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }

        public List<CountryDTO> Countries { get; set; }

        [Inject]
        public ICountriesDataService CountriesDataService { get; set; }

        public string ErrorMessage { get; set; }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public bool IsLoading => !string.IsNullOrEmpty(LoadingMessage);

        public string LoadingMessage { get; set; }

        [Inject]
        public ILocalizerService Localizer { get; set; }

        [Inject]
        public IPeopleDataService PeopleDataService { get; set; }

        [Parameter]
        public Guid PersonId { get; set; }

        [Parameter]
        public string ReturnUrl { get; set; }

        [BindProperty]
        public UpdatePersonDTO UpdatePerson { get; set; }

        public void HideError()
        {
            ErrorMessage = null;
            StateHasChanged();
        }

        public async Task HandleValidSubmit()
        {
            LoadingMessage = "Saving person details...";
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            ApiResult<PersonDTO> putPersonResult =
                await PeopleDataService.UpdatePerson(PersonId, UpdatePerson, authToken);

            if (putPersonResult.IsSuccess)
            {
                LoadingMessage = null;
                await BlazoredModal.Close(ModalResult.Ok(putPersonResult.Object));
            }
            else
            {
                LoadingMessage = null;
                ErrorMessage =
                    $"Failed to save person details. {Localizer[putPersonResult.ErrorResponseCode.ToString()]}";
                StateHasChanged();
            }
        }

        public async Task Cancel()
        {
            await BlazoredModal.Close(ModalResult.Cancel());
        }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            LoadingMessage = "Loading person details...";
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            ApiResult<List<CountryDTO>> getCountriesResult = await CountriesDataService.GetCountries(authToken);

            if (getCountriesResult.IsSuccess)
            {
                Countries = getCountriesResult.Object;
            }
            else
            {
                LoadingMessage = null;
                ErrorMessage =
                    $"Failed to load countries list. {Localizer[getCountriesResult.ErrorResponseCode.ToString()]}";
                StateHasChanged();
                return;
            }

            ApiResult<PersonDTO> getPersonResult = await PeopleDataService.GetPerson(PersonId, authToken);

            if (getPersonResult.IsSuccess)
            {
                UpdatePerson = new UpdatePersonDTO
                {
                    FirstName = getPersonResult.Object.FirstName,
                    MiddleName = getPersonResult.Object.MiddleName,
                    LastName = getPersonResult.Object.LastName,
                    Email = getPersonResult.Object.Email,
                    MobileNo = getPersonResult.Object.MobileNo,
                    Address = new UpdateAddressDTO
                    {
                        Line1 = getPersonResult.Object.Address.Line1,
                        Line2 = getPersonResult.Object.Address.Line2,
                        Line3 = getPersonResult.Object.Address.Line3,
                        Town = getPersonResult.Object.Address.Town,
                        County = getPersonResult.Object.Address.County,
                        PostCode = getPersonResult.Object.Address.PostCode,
                        CountryCode = getPersonResult.Object.Address.CountryCode,
                        LandlinePhone = getPersonResult.Object.Address.LandlinePhone
                    }
                };

                LoadingMessage = null;
                StateHasChanged();
            }
            else
            {
                LoadingMessage = null;
                ErrorMessage =
                    $"Failed to get person details. {Localizer[getPersonResult.ErrorResponseCode.ToString()]}";
                StateHasChanged();
            }
        }
    }
}