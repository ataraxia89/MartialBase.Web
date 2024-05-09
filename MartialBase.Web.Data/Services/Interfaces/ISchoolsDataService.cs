// <copyright file="ISchoolsDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MartialBase.API.Models.DTOs.Addresses;
using MartialBase.API.Models.DTOs.Documents;
using MartialBase.API.Models.DTOs.People;
using MartialBase.API.Models.DTOs.Schools;
using MartialBase.Web.Data.Utilities;

namespace MartialBase.Web.Data.Services.Interfaces
{
    public interface ISchoolsDataService
    {
        Task<ApiResult<List<SchoolDTO>>> GetSchools(string token, Guid? artId = null, Guid? organisationId = null);

        Task<ApiResult<SchoolDTO>> GetSchool(Guid schoolId, string token);

        Task<ApiResult<SchoolDTO>> CreateSchool(CreateSchoolDTO createSchoolDTO, string token);

        Task<ApiResult<SchoolDTO>> UpdateSchool(Guid schoolId, UpdateSchoolDTO updateSchoolDTO, string token);

        Task<ApiResult<AddressDTO>> AddNewAddressToSchool(Guid schoolId, CreateAddressDTO createAddressDTO, string token);

        Task<ApiResult> RemoveAddressFromSchool(Guid schoolId, Guid addressId, string token);

        Task<ApiResult> AddStudentToSchool(
            Guid schoolId, Guid studentId, string token, bool? isInstructor = null, bool? isSecretary = null);

        Task<ApiResult<DocumentDTO>> GetSchoolStudentInsurance(Guid schoolId, Guid studentId, string token);

        Task<ApiResult<DocumentDTO>> UpdateSchoolStudentInsurance(
            Guid schoolId, Guid studentId, CreateDocumentDTO createDocumentDTO, string token, bool? archiveExisting = null);

        Task<ApiResult<DocumentDTO>> GetSchoolStudentLicence(Guid schoolId, Guid studentId, string token);

        Task<ApiResult<DocumentDTO>> UpdateSchoolStudentLicence(
            Guid schoolId, Guid studentId, CreateDocumentDTO createDocumentDTO, string token, bool? archiveExisting = null);

        Task<ApiResult> RemoveStudentFromSchool(Guid schoolId, Guid studentId, string token);

        Task<ApiResult<List<SchoolStudentDTO>>> GetSchoolStudents(Guid schoolId, string token);

        Task<ApiResult> ChangeSchoolOrganisation(Guid schoolId, Guid organisationId, string token);

        Task<ApiResult> ChangeSchoolHeadInstructor(Guid schoolId, Guid studentId, bool retainSecretary, string token);

        Task<ApiResult> ChangeSchoolArt(Guid schoolId, Guid artId, string token);

        Task<ApiResult> DeleteSchool(Guid schoolId, string token);
    }
}