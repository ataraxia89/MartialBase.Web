// <copyright file="FindPersonDialogBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using MartialBase.API.Models.DTOs.People;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace MartialBase.Web.App.Components.People
{
    public class FindPersonDialogBase : ComponentBase
    {
        [Inject]
        public IAuthTokensService AuthTokensService { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }

        public string ErrorMessage { get; set; }

        public List<PersonDTO> FoundPeople { get; set; }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public bool IsSearching { get; set; }

        [Inject]
        public ILocalizerService Localizer { get; set; }

        [Inject]
        public IPeopleDataService PeopleDataService { get; set; }

        [BindProperty]
        public string SearchEmail { get; set; }

        [BindProperty]
        public string SearchFirstName { get; set; }

        [BindProperty]
        public string SearchLastName { get; set; }

        [BindProperty]
        public string SearchMiddleName { get; set; }

        public async Task Search()
        {
            FoundPeople = new List<PersonDTO>();
            ErrorMessage = null;
            IsSearching = true;
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            ApiResult<List<PersonDTO>> findPeopleResult = await PeopleDataService.FindPeople(
                authToken,
                !string.IsNullOrEmpty(SearchEmail) ? SearchEmail : null,
                !string.IsNullOrEmpty(SearchFirstName) ? SearchFirstName : null,
                !string.IsNullOrEmpty(SearchMiddleName) ? SearchMiddleName : null,
                !string.IsNullOrEmpty(SearchLastName) ? SearchLastName : null,
                true);

            if (findPeopleResult.IsSuccess)
            {
                FoundPeople = findPeopleResult.Object;
            }
            else
            {
                ErrorMessage =
                    $"Failed to carry out search. {Localizer[findPeopleResult.ErrorResponseCode.ToString()]}";
            }

            IsSearching = false;
            StateHasChanged();
        }

        public async Task Cancel()
        {
            await BlazoredModal.Close(ModalResult.Cancel());
        }

        public async Task SelectPerson(string personId)
        {
            await BlazoredModal.Close(ModalResult.Ok(new Guid(personId)));
        }

        /// <inheritdoc />
        protected override Task OnInitializedAsync()
        {
            FoundPeople = new List<PersonDTO>();

            return base.OnInitializedAsync();
        }
    }
}