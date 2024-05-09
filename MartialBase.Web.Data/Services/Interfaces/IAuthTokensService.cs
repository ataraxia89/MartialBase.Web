// <copyright file="IAuthTokensService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace MartialBase.Web.Data.Services.Interfaces
{
    public interface IAuthTokensService
    {
        Task<string> GetToken();
    }
}
