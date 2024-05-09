// <copyright file="CountriesDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.Countries;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

namespace MartialBase.Web.Data.Services
{
    public class CountriesDataService : ICountriesDataService
    {
        /// <inheritdoc />
        public async Task<ApiResult<List<CountryDTO>>> GetCountries(string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                "countries",
                token);

            return await ApiResult<List<CountryDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<CountryDTO>> GetCountry(string countryCode, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "countries", countryCode },
                token);

            return await ApiResult<CountryDTO>.GenerateAPIResult(response);
        }
    }
}