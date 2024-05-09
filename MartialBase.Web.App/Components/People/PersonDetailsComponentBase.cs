// <copyright file="PersonDetailsComponentBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.People;
using MartialBase.Web.App.Pages;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace MartialBase.Web.App.Components.People
{
    public class PersonDetailsComponentBase : MartialBasePageBase
    {
        [Inject]
        public IAuthTokensService AuthTokensService { get; set; }

        [Inject]
        public ILocalizerService Localizer { get; set; }

        [BindProperty]
        public PersonDTO Person { get; set; }

        [Inject]
        public IPeopleDataService PeopleDataService { get; set; }

        [CascadingParameter(Name = "PersonId")]
        public Guid? PersonId { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            LoadingMessage = Localizer["LoadingPersonDetails"];
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            if (PersonId != null)
            {
                ApiResult<PersonDTO> getPersonResult = await PeopleDataService.GetPerson((Guid)PersonId, authToken);

                if (getPersonResult.IsSuccess)
                {
                    Person = getPersonResult.Object;

                    LoadingMessage = null;
                    StateHasChanged();
                }
                else
                {
                    LoadingMessage = null;
                    ErrorMessage =
                        $"{Localizer["FailedToLoadPersonDetails"]} {Localizer[getPersonResult.ErrorResponseCode.ToString()]}";
                    StateHasChanged();
                }
            }
        }
    }
}
