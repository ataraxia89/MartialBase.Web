// <copyright file="PeopleDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.People;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

using Microsoft.AspNetCore.Http;

namespace MartialBase.Web.Data.Services
{
    public class PeopleDataService : IPeopleDataService
    {
        /// <inheritdoc />
        public async Task<ApiResult<Guid?>> GetPersonIdForCurrentApplicationUser(string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "people", "getmyid" },
                token);

            return await ApiResult<Guid?>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<Guid?>> GetPersonIdForApplicationUser(string userId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "people", "getid" },
                new Dictionary<string, string> { { "userId", userId } },
                token);

            return await ApiResult<Guid?>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<PersonDTO>> GetPerson(Guid personId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "people", personId.ToString() },
                token);

            return await ApiResult<PersonDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<List<PersonDTO>>> FindPeople(string token, string email = null, string firstName = null, string middleName = null, string lastName = null, bool? returnAddresses = null)
        {
            var searchParameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(email))
            {
                searchParameters.Add("email", email);
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                searchParameters.Add("firstName", firstName);
            }

            if (!string.IsNullOrEmpty(middleName))
            {
                searchParameters.Add("middleName", middleName);
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                searchParameters.Add("lastName", lastName);
            }

            if (returnAddresses != null)
            {
                searchParameters.Add("returnAddresses", returnAddresses.ToString().ToLower());
            }

            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                "people",
                searchParameters,
                token);

            return await ApiResult<List<PersonDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<PersonDTO>> CreatePerson(CreatePersonDTO createPersonDTO, Guid? organisationId, Guid? schoolId, string token)
        {
            var queryParameters = new Dictionary<string, string>();

            if (organisationId != null)
            {
                queryParameters.Add("organisationId", organisationId.ToString());
            }

            if (schoolId != null)
            {
                queryParameters.Add("schoolId", schoolId.ToString());
            }

            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Post,
                "people",
                queryParameters,
                createPersonDTO,
                token);

            return await ApiResult<PersonDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<PersonDTO>> UpdatePerson(Guid personId, UpdatePersonDTO updatePersonDTO, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Put,
                new[] { "people", personId.ToString() },
                updatePersonDTO,
                token);

            return await ApiResult<PersonDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<List<PersonOrganisationDTO>>> GetPersonOrganisations(Guid personId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "people", personId.ToString(), "organisations" },
                token);

            return await ApiResult<List<PersonOrganisationDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<List<StudentSchoolDTO>>> GetStudentSchools(Guid personId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "people", personId.ToString(), "schools" },
                token);

            return await ApiResult<List<StudentSchoolDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> DeletePerson(Guid personId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Delete,
                new[] { "people", personId.ToString() },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public string GetNameFromHttpContext(HttpContext httpContext)
        {
            return httpContext?.User.Identity?.Name;
        }
    }
}