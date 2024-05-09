// <copyright file="ConfirmationPageBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Linq;
using System.Threading.Tasks;
using MartialBase.Web.Data.Services.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace MartialBase.Web.App.Shared
{
    /// <summary>
    /// A component base class to be used for the confirmation page.
    /// </summary>
    public class ConfirmationPageBase : ComponentBase
    {
        [BindProperty]
        public string ConfirmationMessage { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IStringLocalizer{T}"/>.
        /// </summary>
        [Inject]
        public ILocalizerService Localizer { get; set; }

        /// <summary>
        /// Gets or sets the reason or justification of the confirmation message.
        /// </summary>
        [Parameter]
        public string R { get; set; }

        /// <summary>
        /// Gets or sets the type of confirmation message.
        /// </summary>
        [Parameter]
        public string T { get; set; }

        [BindProperty]
        public bool HasReturnLink { get; set; }

        [BindProperty]
        public string ClickHereString => Localizer[nameof(Data.Properties.Resources.ClickHere)];

        [BindProperty]
        public string ActionString { get; set; }

        [BindProperty]
        public string ReturnLink { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <inheritdoc />
        protected override Task OnInitializedAsync()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("t", out var confirmationType))
            {
                T = confirmationType.First();
            }

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("r", out var reason))
            {
                R = reason.First();
            }

            ConfirmationMessage = Localizer[T];

            if (!string.IsNullOrEmpty(R))
            {
                ConfirmationMessage += $" {Localizer[R]}";
            }

            if (T == nameof(Data.Properties.Resources.EmailConfirmationSuccessful) ||
                T == nameof(Data.Properties.Resources.EmailNotConfirmed) ||
                T == nameof(Data.Properties.Resources.ExpiredToken) ||
                T == nameof(Data.Properties.Resources.ResetSuccess) ||
                T == nameof(Data.Properties.Resources.LogoutSuccessful) ||
                T == nameof(Data.Properties.Resources.LoginFailed))
            {
                HasReturnLink = true;

                ReturnLink = "/MicrosoftIdentity/Account/SignIn";

                ActionString = Localizer[nameof(Data.Properties.Resources.ToReturnToLogin)];
            }
            else
            {
                HasReturnLink = false;
            }

            return base.OnInitializedAsync();
        }
    }
}