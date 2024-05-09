// <copyright file="AuthDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

namespace MartialBase.Web.Data.Services
{
    public class AuthDataService : IAuthDataService
    {
        /// <inheritdoc />
        public async Task<ApiResult> LockOutUser(string userId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Delete,
                "login",
                new Dictionary<string, string> { { "userId", userId } },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }
    }
}