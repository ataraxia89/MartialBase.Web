// <copyright file="IAuthDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Threading.Tasks;

using MartialBase.Web.Data.Utilities;

namespace MartialBase.Web.Data.Services.Interfaces
{
    public interface IAuthDataService
    {
        Task<ApiResult> LockOutUser(string userId, string token);
    }
}