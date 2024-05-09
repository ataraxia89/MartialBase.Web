// <copyright file="SchoolsDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.Addresses;
using MartialBase.API.Models.DTOs.Documents;
using MartialBase.API.Models.DTOs.People;
using MartialBase.API.Models.DTOs.Schools;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

namespace MartialBase.Web.Data.Services
{
    public class SchoolsDataService : ISchoolsDataService
    {
        private enum StudentDocumentType
        {
            Insurance,
            Licence
        }

        /// <inheritdoc />
        public async Task<ApiResult<List<SchoolDTO>>> GetSchools(string token, Guid? artId = null, Guid? organisationId = null)
        {
            var queryParameters = new Dictionary<string, string>();

            if (artId != null)
            {
                queryParameters.Add("artId", artId.ToString());
            }

            if (organisationId != null)
            {
                queryParameters.Add("organisationId", organisationId.ToString());
            }

            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                "schools",
                queryParameters,
                token);

            return await ApiResult<List<SchoolDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<SchoolDTO>> GetSchool(Guid schoolId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "schools", schoolId.ToString() },
                token);

            return await ApiResult<SchoolDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<SchoolDTO>> CreateSchool(CreateSchoolDTO createSchoolDTO, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Post,
                "schools",
                createSchoolDTO,
                token);

            return await ApiResult<SchoolDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<SchoolDTO>> UpdateSchool(Guid schoolId, UpdateSchoolDTO updateSchoolDTO, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Put,
                new[] { "schools", schoolId.ToString() },
                updateSchoolDTO,
                token);

            return await ApiResult<SchoolDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<AddressDTO>> AddNewAddressToSchool(Guid schoolId, CreateAddressDTO createAddressDTO, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Post,
                new[] { "schools", schoolId.ToString(), "addresses" },
                createAddressDTO,
                token);

            return await ApiResult<AddressDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> RemoveAddressFromSchool(Guid schoolId, Guid addressId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Delete,
                new[] { "schools", schoolId.ToString(), "addresses", addressId.ToString() },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> AddStudentToSchool(Guid schoolId, Guid studentId, string token, bool? isInstructor = null, bool? isSecretary = null)
        {
            var queryParameters = new Dictionary<string, string> { { "studentId", studentId.ToString() } };

            if (isInstructor != null)
            {
                queryParameters.Add("isInstructor", isInstructor.ToString().ToLower());
            }

            if (isSecretary != null)
            {
                queryParameters.Add("isSecretary", isSecretary.ToString().ToLower());
            }

            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Post,
                new[] { "schools", schoolId.ToString(), "students" },
                queryParameters,
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<DocumentDTO>> GetSchoolStudentInsurance(Guid schoolId, Guid studentId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "schools", schoolId.ToString(), "students", studentId.ToString(), "insurance" },
                token);

            return await ApiResult<DocumentDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<DocumentDTO>> UpdateSchoolStudentInsurance(
            Guid schoolId, Guid studentId, CreateDocumentDTO createDocumentDTO, string token, bool? archiveExisting = null)
        {
            return await UpdateStudentDocument(
                StudentDocumentType.Insurance, schoolId, studentId, createDocumentDTO, archiveExisting, token);
        }

        /// <inheritdoc />
        public async Task<ApiResult<DocumentDTO>> GetSchoolStudentLicence(Guid schoolId, Guid studentId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "schools", schoolId.ToString(), "students", studentId.ToString(), "licence" },
                token);

            return await ApiResult<DocumentDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<DocumentDTO>> UpdateSchoolStudentLicence(
            Guid schoolId, Guid studentId, CreateDocumentDTO createDocumentDTO, string token, bool? archiveExisting = null)
        {
            return await UpdateStudentDocument(
                StudentDocumentType.Licence, schoolId, studentId, createDocumentDTO, archiveExisting, token);
        }

        /// <inheritdoc />
        public async Task<ApiResult> RemoveStudentFromSchool(Guid schoolId, Guid studentId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Delete,
                new[] { "schools", schoolId.ToString(), "students", studentId.ToString() },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<List<SchoolStudentDTO>>> GetSchoolStudents(Guid schoolId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "schools", schoolId.ToString(), "students" },
                token);

            return await ApiResult<List<SchoolStudentDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> ChangeSchoolOrganisation(Guid schoolId, Guid organisationId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Put,
                new[] { "schools", schoolId.ToString(), "organisation" },
                new Dictionary<string, string> { { "organisationId", organisationId.ToString() } },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> ChangeSchoolHeadInstructor(Guid schoolId, Guid studentId, bool retainSecretary, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Put,
                new[] { "schools", schoolId.ToString(), "headinstructor" },
                new Dictionary<string, string>
                {
                    { "studentId", studentId.ToString() },
                    { "retainSecretary", retainSecretary.ToString().ToLower() }
                },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> ChangeSchoolArt(Guid schoolId, Guid artId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Put,
                new[] { "schools", schoolId.ToString(), "art" },
                new Dictionary<string, string> { { "artId", artId.ToString() } },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> DeleteSchool(Guid schoolId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Delete,
                new[] { "schools", schoolId.ToString() },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        private async Task<ApiResult<DocumentDTO>> UpdateStudentDocument(
            StudentDocumentType studentDocumentType, Guid schoolId, Guid studentId, CreateDocumentDTO createDocumentDTO, bool? archiveExisting, string token)
        {
            var queryParameters = new Dictionary<string, string>();

            if (archiveExisting != null)
            {
                queryParameters.Add("archiveExisting", archiveExisting.ToString().ToLower());
            }

            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Post,
                new[] { "schools", schoolId.ToString(), "students", studentId.ToString(), studentDocumentType.ToString().ToLower() },
                queryParameters,
                createDocumentDTO,
                token);

            return await ApiResult<DocumentDTO>.GenerateAPIResult(response);
        }
    }
}