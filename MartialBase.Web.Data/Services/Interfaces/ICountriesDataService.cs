// <copyright file="ICountriesDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using MartialBase.API.Models.DTOs.Countries;
using MartialBase.Web.Data.Utilities;

namespace MartialBase.Web.Data.Services.Interfaces
{
    public interface ICountriesDataService
    {
        Task<ApiResult<List<CountryDTO>>> GetCountries(string token);

        Task<ApiResult<CountryDTO>> GetCountry(string countryCode, string token);
    }
}