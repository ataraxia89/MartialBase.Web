// <copyright file="PeopleDataServiceMock.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.People;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;
using MartialBase.Web.MockData.DataGenerators;
using MartialBase.Web.MockData.Tools;

using Microsoft.AspNetCore.Http;

namespace MartialBase.Web.MockData.Services;

public class PeopleDataServiceMock : IPeopleDataService
{
    /// <inheritdoc />
    public async Task<ApiResult<Guid?>> GetPersonIdForCurrentApplicationUser(string token)
    {
        var personId = Guid.NewGuid();
        var response = HttpResponseGenerator.GetResponseMessage(personId);

        return await ApiResult<Guid?>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<Guid?>> GetPersonIdForApplicationUser(string userId, string token)
    {
        var personId = Guid.NewGuid();
        var response = HttpResponseGenerator.GetResponseMessage(personId);

        return await ApiResult<Guid?>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<PersonDTO>> GetPerson(Guid personId, string token)
    {
        var person = People.GeneratePersonDTO(personId);
        var response = HttpResponseGenerator.GetResponseMessage(person);

        return await ApiResult<PersonDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<List<PersonDTO>>> FindPeople(string token, string email = null, string firstName = null, string middleName = null, string lastName = null, bool? returnAddresses = null)
    {
        var people = People.GeneratePersonDTOs(10, email, firstName, middleName, lastName);
        var response = HttpResponseGenerator.GetResponseMessage(people);

        return await ApiResult<List<PersonDTO>>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<PersonDTO>> CreatePerson(CreatePersonDTO createPersonDTO, Guid? organisationId, Guid? schoolId, string token)
    {
        var personDTO = People.GetPersonDTOFromCreateDTO(createPersonDTO);
        var response = HttpResponseGenerator.GetResponseMessage(personDTO);

        return await ApiResult<PersonDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<PersonDTO>> UpdatePerson(Guid personId, UpdatePersonDTO updatePersonDTO, string token)
    {
        var person = People.GetPersonDTOFromUpdateDTO(personId, updatePersonDTO);
        var response = HttpResponseGenerator.GetResponseMessage(person);

        return await ApiResult<PersonDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<List<PersonOrganisationDTO>>> GetPersonOrganisations(Guid personId, string token)
    {
        var personOrganisations = Organisations.GeneratePersonOrganisationDTOs(5, personId);
        var response = HttpResponseGenerator.GetResponseMessage(personOrganisations);

        return await ApiResult<List<PersonOrganisationDTO>>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<List<StudentSchoolDTO>>> GetStudentSchools(Guid personId, string token)
    {
        var studentSchools = Schools.GenerateStudentSchoolDTOs(10);
        var response = HttpResponseGenerator.GetResponseMessage(studentSchools);
        
        return await ApiResult<List<StudentSchoolDTO>>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> DeletePerson(Guid personId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.NoContent);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public string GetNameFromHttpContext(HttpContext httpContext)
    {
        return FakeData.Name.First();
    }
}