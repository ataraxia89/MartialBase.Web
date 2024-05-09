// <copyright file="ILocalizerService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

namespace MartialBase.Web.Data.Services.Interfaces
{
    public interface ILocalizerService
    {
        string this[string index] { get; }
    }
}