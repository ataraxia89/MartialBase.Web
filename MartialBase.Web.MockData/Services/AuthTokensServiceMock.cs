// <copyright file="AuthTokensServiceMock.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Threading.Tasks;

using MartialBase.Web.Data.Services.Interfaces;

namespace MartialBase.Web.MockData.Services;

public class AuthTokensServiceMock : IAuthTokensService
{
    /// <inheritdoc />
    public Task<string> GetToken() => Task.FromResult(string.Empty);
}