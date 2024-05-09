// <copyright file="NotAuthorizedComponentBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MartialBase.Web.App.Shared
{
    public class NotAuthorizedComponentBase : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        /// <inheritdoc />
        protected override async void OnInitialized()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (!user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("MicrosoftIdentity/Account/SignIn", true);
            }
        }
    }
}