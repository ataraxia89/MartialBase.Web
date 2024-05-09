// <copyright file="SchoolsDataServiceMock.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.Addresses;
using MartialBase.API.Models.DTOs.Documents;
using MartialBase.API.Models.DTOs.People;
using MartialBase.API.Models.DTOs.Schools;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;
using MartialBase.Web.MockData.DataGenerators;
using MartialBase.Web.MockData.Tools;

namespace MartialBase.Web.MockData.Services;

public class SchoolsDataServiceMock : ISchoolsDataService
{
    /// <inheritdoc />
    public async Task<ApiResult<List<SchoolDTO>>> GetSchools(string token, Guid? artId = null, Guid? organisationId = null)
    {
        var schools = Schools.GenerateSchoolDTOs(10, artId, organisationId);
        var response = HttpResponseGenerator.GetResponseMessage(schools);

        return await ApiResult<List<SchoolDTO>>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<SchoolDTO>> GetSchool(Guid schoolId, string token)
    {
        var school = Schools.GenerateSchoolDTO(schoolId: schoolId);
        var response = HttpResponseGenerator.GetResponseMessage(school);

        return await ApiResult<SchoolDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<SchoolDTO>> CreateSchool(CreateSchoolDTO createSchoolDTO, string token)
    {
        var school = Schools.GetSchoolDTOFromCreateDTO(createSchoolDTO);
        var response = HttpResponseGenerator.GetResponseMessage(school);

        return await ApiResult<SchoolDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<SchoolDTO>> UpdateSchool(Guid schoolId, UpdateSchoolDTO updateSchoolDTO, string token)
    {
        var school = Schools.GetSchoolDTOFromUpdateDTO(schoolId, updateSchoolDTO);
        var response = HttpResponseGenerator.GetResponseMessage(school);

        return await ApiResult<SchoolDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<AddressDTO>> AddNewAddressToSchool(Guid schoolId, CreateAddressDTO createAddressDTO, string token)
    {
        var address = Addresses.GetAddressDTOFromCreateDTO(createAddressDTO);
        var response = HttpResponseGenerator.GetResponseMessage(address);

        return await ApiResult<AddressDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> RemoveAddressFromSchool(Guid schoolId, Guid addressId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.NoContent);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> AddStudentToSchool(Guid schoolId, Guid studentId, string token, bool? isInstructor = null, bool? isSecretary = null)
    {
        var response = new HttpResponseMessage(HttpStatusCode.Created);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<DocumentDTO>> GetSchoolStudentInsurance(Guid schoolId, Guid studentId, string token)
    {
        var document = Documents.GenerateDocumentDTO("Insurance Certificate");
        var response = HttpResponseGenerator.GetResponseMessage(document);

        return await ApiResult<DocumentDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<DocumentDTO>> UpdateSchoolStudentInsurance(Guid schoolId, Guid studentId, CreateDocumentDTO createDocumentDTO, string token, bool? archiveExisting = null)
    {
        var document = Documents.GetDocumentDTOFromCreateDTO(createDocumentDTO, "Insurance Certificate");
        var response = HttpResponseGenerator.GetResponseMessage(document);

        return await ApiResult<DocumentDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<DocumentDTO>> GetSchoolStudentLicence(Guid schoolId, Guid studentId, string token)
    {
        var document = Documents.GenerateDocumentDTO("Licence Certificate");
        var response = HttpResponseGenerator.GetResponseMessage(document);

        return await ApiResult<DocumentDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<DocumentDTO>> UpdateSchoolStudentLicence(Guid schoolId, Guid studentId, CreateDocumentDTO createDocumentDTO, string token, bool? archiveExisting = null)
    {
        var document = Documents.GetDocumentDTOFromCreateDTO(createDocumentDTO, "Licence Certificate");
        var response = HttpResponseGenerator.GetResponseMessage(document);

        return await ApiResult<DocumentDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> RemoveStudentFromSchool(Guid schoolId, Guid studentId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.NoContent);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<List<SchoolStudentDTO>>> GetSchoolStudents(Guid schoolId, string token)
    {
        var schoolStudents = People.GenerateSchoolStudentDTOs(20, schoolId);
        var response = HttpResponseGenerator.GetResponseMessage(schoolStudents);

        return await ApiResult<List<SchoolStudentDTO>>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> ChangeSchoolOrganisation(Guid schoolId, Guid organisationId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> ChangeSchoolHeadInstructor(Guid schoolId, Guid studentId, bool retainSecretary, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> ChangeSchoolArt(Guid schoolId, Guid artId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> DeleteSchool(Guid schoolId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.NoContent);

        return await ApiResult.GenerateAPIResult(response);
    }
}