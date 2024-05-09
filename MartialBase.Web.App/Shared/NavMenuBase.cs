// <copyright file="NavMenuBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Components;

namespace MartialBase.Web.App.Shared
{
    /// <summary>
    /// A view model class to be used for the navigation menu.
    /// </summary>
    public class NavMenuBase : ComponentBase
    {
        private bool collapseNavMenu = true;

        public string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        public void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}