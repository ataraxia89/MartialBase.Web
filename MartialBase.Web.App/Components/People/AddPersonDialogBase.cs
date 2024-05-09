// <copyright file="AddPersonDialogBase.cs" company="Martialtech®">
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
using MartialBase.API.Models.DTOs.Organisations;
using MartialBase.API.Models.DTOs.People;
using MartialBase.API.Models.DTOs.Schools;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace MartialBase.Web.App.Components.People
{
    public class AddPersonDialogBase : ComponentBase
    {
        [Parameter]
        public string MartialBaseUserId { get; set; }

        [Inject]
        public IAuthTokensService AuthTokensService { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }

        public bool CanSelectOrganisation { get; set; }

        public bool CanSelectSchool { get; set; }

        public List<CountryDTO> Countries { get; set; }

        [Inject]
        public ICountriesDataService CountriesDataService { get; set; }

        [BindProperty]
        public CreatePersonDTO CreatePerson { get; set; }

        [BindProperty]
        public DateTime DateOfBirth { get; set; }

        public string ErrorMessage { get; set; }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public bool IsLoading => !string.IsNullOrEmpty(LoadingMessage);

        public string LoadingMessage { get; set; }

        public bool LoadingSchools { get; set; }

        [Inject]
        public ILocalizerService Localizer { get; set; }

        [Parameter]
        public Guid? OrganisationId { get; set; }

        public List<OrganisationDTO> Organisations { get; set; }

        [Inject]
        public IOrganisationsDataService OrganisationsDataService { get; set; }

        [Inject]
        public IPeopleDataService PeopleDataService { get; set; }

        [Parameter]
        public Guid? SchoolId { get; set; }

        public List<SchoolDTO> Schools { get; set; }

        [Inject]
        public ISchoolsDataService SchoolsDataService { get; set; }

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

            CreatePerson.DateOfBirth = DateOfBirth.ToString("yyyy-MM-dd");

            ApiResult<PersonDTO> postPersonResult =
                await PeopleDataService.CreatePerson(CreatePerson, OrganisationId, SchoolId, authToken);

            if (postPersonResult.IsSuccess)
            {
                LoadingMessage = null;
                await BlazoredModal.Close(ModalResult.Ok(postPersonResult.Object));
            }
            else
            {
                LoadingMessage = null;
                ErrorMessage =
                    $"Failed to save person details. {Localizer[postPersonResult.ErrorResponseCode.ToString()]}";
                StateHasChanged();
            }
        }

        public async Task Cancel()
        {
            ResetPersonObject();
            StateHasChanged();

            await BlazoredModal.Close(ModalResult.Cancel());
        }

        public async Task OnOrganisationChanged(ChangeEventArgs e)
        {
            OrganisationId = new Guid(e.Value.ToString());
            await LoadOrganisationSchools();
        }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            ResetPersonObject();
            LoadingMessage = "Loading countries...";
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

            LoadingMessage = "Loading organisations...";
            StateHasChanged();

            ApiResult<List<OrganisationDTO>> getOrganisationsResult =
                await OrganisationsDataService.GetOrganisations(authToken);

            if (getOrganisationsResult.IsSuccess)
            {
                Organisations = getOrganisationsResult.Object;
                LoadingMessage = null;
            }
            else
            {
                LoadingMessage = null;
                ErrorMessage =
                    $"Failed to load organisations list. {Localizer[getOrganisationsResult.ErrorResponseCode.ToString()]}";
                StateHasChanged();
                return;
            }

            CanSelectOrganisation = true;
            Schools = new List<SchoolDTO>();
            CreatePerson = new CreatePersonDTO
                { Address = new CreateAddressDTO { CountryCode = "GBR" } };
        }

        private async Task LoadOrganisationSchools()
        {
            string authToken = await AuthTokensService.GetToken();

            ApiResult<List<SchoolDTO>> getSchoolsResult =
                await SchoolsDataService.GetSchools(organisationId: OrganisationId, token: authToken);

            if (getSchoolsResult.IsSuccess)
            {
                Schools = getSchoolsResult.Object;
                LoadingSchools = false;
                CanSelectSchool = true;
                StateHasChanged();
            }
        }

        private void ResetPersonObject()
        {
            CreatePerson = new CreatePersonDTO
                { Address = new CreateAddressDTO() };
        }
    }
}