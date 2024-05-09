// <copyright file="IOrganisationsDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MartialBase.API.Models.DTOs.Addresses;
using MartialBase.API.Models.DTOs.DocumentTypes;
using MartialBase.API.Models.DTOs.Organisations;
using MartialBase.API.Models.DTOs.People;
using MartialBase.Web.Data.Utilities;

namespace MartialBase.Web.Data.Services.Interfaces
{
    public interface IOrganisationsDataService
    {
        Task<ApiResult<List<OrganisationDTO>>> GetOrganisations(string token, Guid? parentId = null);

        Task<ApiResult<OrganisationDTO>> GetOrganisation(Guid organisationId, string token);

        Task<ApiResult<OrganisationDTO>> CreateOrganisation(CreateOrganisationDTO createOrganisationDTO, string token);

        Task<ApiResult<PersonOrganisationDTO>> CreateOrganisationWithNewPerson(
            CreatePersonOrganisationDTO createPersonOrganisationDTO, string token);

        Task<ApiResult<OrganisationDTO>> UpdateOrganisation(
            Guid organisationId, UpdateOrganisationDTO updateOrganisationDTO, string token);

        Task<ApiResult> ChangeOrganisationParent(Guid organisationId, Guid parentId, string token);

        Task<ApiResult> RemoveOrganisationParent(Guid organisationId, string token);

        Task<ApiResult<AddressDTO>> ChangeOrganisationAddress(
            Guid organisationId, CreateAddressDTO createAddressDTO, string token);

        Task<ApiResult> AddExistingPersonToOrganisation(
            Guid organisationId, Guid personId, string token, bool? isAdmin = null);

        Task<ApiResult> RemoveOrganisationPerson(Guid organisationId, Guid personId, string token);

        Task<ApiResult<List<OrganisationPersonDTO>>> GetOrganisationPeople(Guid organisationId, string token);

        Task<ApiResult<List<DocumentTypeDTO>>> GetOrganisationDocumentTypes(Guid organisationId, string token);

        Task<ApiResult> DeleteOrganisation(Guid organisationId, string token);
    }
}