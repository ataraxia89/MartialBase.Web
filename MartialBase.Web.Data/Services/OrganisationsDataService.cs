// <copyright file="OrganisationsDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.Addresses;
using MartialBase.API.Models.DTOs.DocumentTypes;
using MartialBase.API.Models.DTOs.Organisations;
using MartialBase.API.Models.DTOs.People;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

namespace MartialBase.Web.Data.Services
{
    public class OrganisationsDataService : IOrganisationsDataService
    {
        /// <inheritdoc />
        public async Task<ApiResult<List<OrganisationDTO>>> GetOrganisations(string token, Guid? parentId = null)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                "organisations",
                token);

            return await ApiResult<List<OrganisationDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<OrganisationDTO>> GetOrganisation(Guid organisationId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "organisations", organisationId.ToString() },
                token);

            return await ApiResult<OrganisationDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<OrganisationDTO>> CreateOrganisation(CreateOrganisationDTO createOrganisationDTO, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Post,
                "organisations",
                createOrganisationDTO,
                token);

            return await ApiResult<OrganisationDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<PersonOrganisationDTO>> CreateOrganisationWithNewPerson(CreatePersonOrganisationDTO createPersonOrganisationDTO, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Post,
                new[] { "organisations", "newperson" },
                createPersonOrganisationDTO,
                token);

            return await ApiResult<PersonOrganisationDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<OrganisationDTO>> UpdateOrganisation(Guid organisationId, UpdateOrganisationDTO updateOrganisationDTO, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Put,
                new[] { "organisations", organisationId.ToString() },
                updateOrganisationDTO,
                token);

            return await ApiResult<OrganisationDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> ChangeOrganisationParent(Guid organisationId, Guid parentId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Put,
                new[] { "organisations", organisationId.ToString(), "parent" },
                new Dictionary<string, string> { { "parentId", parentId.ToString() } },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> RemoveOrganisationParent(Guid organisationId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Delete,
                new[] { "organisations", organisationId.ToString(), "parent" },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<AddressDTO>> ChangeOrganisationAddress(Guid organisationId, CreateAddressDTO createAddressDTO, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Post,
                new[] { "organisations", organisationId.ToString(), "address" },
                createAddressDTO,
                token);

            return await ApiResult<AddressDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> AddExistingPersonToOrganisation(Guid organisationId, Guid personId, string token, bool? isAdmin = null)
        {
            var queryParameters = new Dictionary<string, string> { { "personId", personId.ToString() } };

            if (isAdmin != null)
            {
                queryParameters.Add("isAdmin", isAdmin.ToString().ToLower());
            }

            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Post,
                new[] { "organisations", organisationId.ToString(), "people" },
                queryParameters,
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> RemoveOrganisationPerson(Guid organisationId, Guid personId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Delete,
                new[] { "organisations", organisationId.ToString(), "people", personId.ToString() },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<List<OrganisationPersonDTO>>> GetOrganisationPeople(Guid organisationId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "organisations", organisationId.ToString(), "people" },
                token);

            return await ApiResult<List<OrganisationPersonDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<List<DocumentTypeDTO>>> GetOrganisationDocumentTypes(Guid organisationId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "organisations", organisationId.ToString(), "documenttypes" },
                token);

            return await ApiResult<List<DocumentTypeDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> DeleteOrganisation(Guid organisationId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Delete,
                new[] { "organisations", organisationId.ToString() },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }
    }
}