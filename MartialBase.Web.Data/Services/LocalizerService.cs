// <copyright file="LocalizerService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using MartialBase.Web.Data.Services.Interfaces;

namespace MartialBase.Web.Data.Services
{
    public class LocalizerService : ILocalizerService
    {
        public LocalizerService()
        {
        }

        public string this[string index] => Properties.Resources.ResourceManager.GetString(index) ?? index;
    }
}