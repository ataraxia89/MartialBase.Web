// <copyright file="OrganisationsDataServiceMock.cs" company="Martialtech®">
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
using MartialBase.API.Models.DTOs.DocumentTypes;
using MartialBase.API.Models.DTOs.Organisations;
using MartialBase.API.Models.DTOs.People;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;
using MartialBase.Web.MockData.DataGenerators;
using MartialBase.Web.MockData.Tools;

namespace MartialBase.Web.MockData.Services;

public class OrganisationsDataServiceMock : IOrganisationsDataService
{
    /// <inheritdoc />
    public async Task<ApiResult<List<OrganisationDTO>>> GetOrganisations(string token, Guid? parentId = null)
    {
        var organisations = Organisations.GenerateOrganisationDTOs(20);
        var response = HttpResponseGenerator.GetResponseMessage(organisations);

        return await ApiResult<List<OrganisationDTO>>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<OrganisationDTO>> GetOrganisation(Guid organisationId, string token)
    {
        var organisation = Organisations.GenerateOrganisationDTO();
        var response = HttpResponseGenerator.GetResponseMessage(organisation);

        return await ApiResult<OrganisationDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<OrganisationDTO>> CreateOrganisation(CreateOrganisationDTO createOrganisationDTO, string token)
    {
        var organisation = Organisations.GetOrganisationDTOFromCreateDTO(createOrganisationDTO);
        var response = HttpResponseGenerator.GetResponseMessage(organisation, HttpStatusCode.Created);

        return await ApiResult<OrganisationDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<PersonOrganisationDTO>> CreateOrganisationWithNewPerson(CreatePersonOrganisationDTO createPersonOrganisationDTO, string token)
    {
        var personOrganisationDTO = Organisations.GetPersonOrganisationDTOFromCreateDTO(createPersonOrganisationDTO);
        var response = HttpResponseGenerator.GetResponseMessage(personOrganisationDTO);

        return await ApiResult<PersonOrganisationDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<OrganisationDTO>> UpdateOrganisation(Guid organisationId, UpdateOrganisationDTO updateOrganisationDTO, string token)
    {
        var organisation = Organisations.GetOrganisationDTOFromUpdateDTO(updateOrganisationDTO, organisationId);
        var response = HttpResponseGenerator.GetResponseMessage(organisation);

        return await ApiResult<OrganisationDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> ChangeOrganisationParent(Guid organisationId, Guid parentId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> RemoveOrganisationParent(Guid organisationId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.NoContent);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<AddressDTO>> ChangeOrganisationAddress(Guid organisationId, CreateAddressDTO createAddressDTO, string token)
    {
        var address = Addresses.GetAddressDTOFromCreateDTO(createAddressDTO);
        var response = HttpResponseGenerator.GetResponseMessage(address);

        return await ApiResult<AddressDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> AddExistingPersonToOrganisation(Guid organisationId, Guid personId, string token, bool? isAdmin = null)
    {
        var response = new HttpResponseMessage(HttpStatusCode.Created);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> RemoveOrganisationPerson(Guid organisationId, Guid personId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.NoContent);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<List<OrganisationPersonDTO>>> GetOrganisationPeople(Guid organisationId, string token)
    {
        var organisationPeople = People.GenerateOrganisationPersonDTOs(20);
        var response = HttpResponseGenerator.GetResponseMessage(organisationPeople);

        return await ApiResult<List<OrganisationPersonDTO>>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<List<DocumentTypeDTO>>> GetOrganisationDocumentTypes(Guid organisationId, string token)
    {
        var documentTypes = DocumentTypes.GenerateDocumentTypeDTOs(10, organisationId);
        var response = HttpResponseGenerator.GetResponseMessage(documentTypes);

        return await ApiResult<List<DocumentTypeDTO>>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> DeleteOrganisation(Guid organisationId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.NoContent);

        return await ApiResult.GenerateAPIResult(response);
    }
}