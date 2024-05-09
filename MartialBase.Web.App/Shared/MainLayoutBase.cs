// <copyright file="MainLayoutBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;

using MartialBase.Web.Data.Services.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartialBase.Web.App.Shared
{
    /// <summary>
    /// A view model class to be used for the main layout.
    /// </summary>
    public class MainLayoutBase : LayoutComponentBase
    {
        [Inject]
        public IAuthTokensService AuthTokensService { get; set; }

        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        [Inject]
        public ILocalizerService Localizer { get; set; }

        [Inject]
        public IPeopleDataService PeopleDataService { get; set; }

        [BindProperty]
        public string WelcomeMessage { get; set; }

        /// <inheritdoc />
        protected override Task OnInitializedAsync()
        {
            var currentHour = DateTime.Now.Hour;
            var name = PeopleDataService.GetNameFromHttpContext(HttpContextAccessor.HttpContext);

            if (currentHour is > 5 and < 12)
            {
                WelcomeMessage = string.Format(Localizer["GreetingMorning"], name != null ? $" {name}" : string.Empty);
            }
            else if (currentHour is >= 12 and < 17)
            {
                WelcomeMessage = string.Format(Localizer["GreetingAfternoon"], name != null ? $" {name}" : string.Empty);
            }
            else if (currentHour is >= 17 and < 23)
            {
                WelcomeMessage = string.Format(Localizer["GreetingEvening"], name != null ? $" {name}" : string.Empty);
            }
            else
            {
                WelcomeMessage = string.Format(Localizer["GreetingGeneric"], name != null ? $" {name}" : string.Empty);
            }

            return base.OnInitializedAsync();
        }
    }
}