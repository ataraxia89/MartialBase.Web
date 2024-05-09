// <copyright file="AuthDataServiceMock.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

namespace MartialBase.Web.MockData.Services;

public class AuthDataServiceMock : IAuthDataService
{
    /// <inheritdoc />
    public async Task<ApiResult> LockOutUser(string userId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.NoContent);

        return await ApiResult.GenerateAPIResult(response);
    }
}