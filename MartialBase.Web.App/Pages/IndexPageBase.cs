// <copyright file="IndexPageBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;

using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

using Microsoft.AspNetCore.Components;

namespace MartialBase.Web.App.Pages
{
    public class IndexPageBase : ComponentBase
    {
        [Inject]
        public IAuthDataService AuthDataService { get; set; }

        [Inject]
        public IAuthTokensService AuthTokensService { get; set; }

        public Guid? CurrentPersonId { get; set; }

        public string CurrentPersonLoadingMessage { get; set; }

        public bool HasLoadingPersonErrorMessage => !string.IsNullOrEmpty(LoadingPersonErrorMessage);

        public bool IsLoadingCurrentPerson => !string.IsNullOrEmpty(CurrentPersonLoadingMessage) || CurrentPersonId == null;

        public string LoadingPersonErrorMessage { get; set; }

        [Inject]
        public ILocalizerService Localizer { get; set; }

        [Inject]
        public IPeopleDataService PeopleDataService { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            CurrentPersonLoadingMessage = Localizer["RetrievingCurrentUserDetails"];
            StateHasChanged();

            string authToken = await AuthTokensService.GetToken();

            ApiResult<Guid?> getPersonIdResult = await PeopleDataService.GetPersonIdForCurrentApplicationUser(authToken);

            if (getPersonIdResult.IsSuccess)
            {
                CurrentPersonId = getPersonIdResult.Object;
                CurrentPersonLoadingMessage = null;

                if (CurrentPersonId == null)
                {
                    LoadingPersonErrorMessage = Localizer["FailedToRetrieveCurrentUserDetails"];
                }

                StateHasChanged();
            }
            else
            {
                CurrentPersonLoadingMessage = null;
                LoadingPersonErrorMessage =
                    $"{Localizer["FailedToRetrieveCurrentUserDetails"]} {Localizer[getPersonIdResult.ErrorResponseCode.ToString()]}";
                StateHasChanged();
            }
        }
    }
}
