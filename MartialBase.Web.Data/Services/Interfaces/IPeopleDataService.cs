// <copyright file="IPeopleDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.People;
using MartialBase.Web.Data.Utilities;

using Microsoft.AspNetCore.Http;

namespace MartialBase.Web.Data.Services.Interfaces
{
    public interface IPeopleDataService
    {
        Task<ApiResult<Guid?>> GetPersonIdForCurrentApplicationUser(string token);

        Task<ApiResult<Guid?>> GetPersonIdForApplicationUser(string userId, string token);

        Task<ApiResult<PersonDTO>> GetPerson(Guid personId, string token);

        Task<ApiResult<List<PersonDTO>>> FindPeople(
            string token, string email = null, string firstName = null, string middleName = null, string lastName = null, bool? returnAddresses = null);

        Task<ApiResult<PersonDTO>> CreatePerson(
            CreatePersonDTO createPersonDTO, Guid? organisationId, Guid? schoolId, string token);

        Task<ApiResult<PersonDTO>> UpdatePerson(Guid personId, UpdatePersonDTO updatePersonDTO, string token);

        Task<ApiResult<List<PersonOrganisationDTO>>> GetPersonOrganisations(Guid personId, string token);

        Task<ApiResult<List<StudentSchoolDTO>>> GetStudentSchools(Guid personId, string token);

        Task<ApiResult> DeletePerson(Guid personId, string token);

        string GetNameFromHttpContext(HttpContext httpContext);
    }
}