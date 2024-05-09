// <copyright file="AuthTokensService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Threading.Tasks;

using MartialBase.Web.Data.Services.Interfaces;

using Microsoft.Identity.Web;

namespace MartialBase.Web.Data.Services
{
    public class AuthTokensService : IAuthTokensService
    {
        private readonly ITokenAcquisition _tokenAcquisition;

        public AuthTokensService(ITokenAcquisition tokenAcquisition)
        {
            _tokenAcquisition = tokenAcquisition;
        }

        /// <inheritdoc />
        public async Task<string> GetToken()
        {
            return await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { "https://martialbase.net/api/query" });
        }
    }
}
