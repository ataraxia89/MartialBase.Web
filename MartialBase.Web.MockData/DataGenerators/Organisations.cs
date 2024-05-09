// <copyright file="Organisations.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.DataGenerator
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MartialBase.API.Models.DTOs.Organisations;
using MartialBase.API.Models.DTOs.People;
using MartialBase.FakeData;
using MartialBase.Web.MockData.Tools;

namespace MartialBase.Web.MockData.DataGenerators
{
    public static class Organisations
    {
        public static List<OrganisationDTO> GenerateOrganisationDTOs(int numberToGenerate)
        {
            var organisations = new List<OrganisationDTO>();

            for (var i = 0; i < numberToGenerate; i++)
            {
                organisations.Add(GenerateOrganisationDTO());
            }

            return organisations;
        }

        public static OrganisationDTO GenerateOrganisationDTO()
        {
            var name = Company.Name();
            var hasParent = RandomData.GetRandomBool();

            return new OrganisationDTO(
                Guid.NewGuid().ToString(),
                new Regex("[^A-Z]").Replace(name, string.Empty),
                name,
                hasParent ? Guid.NewGuid().ToString() : null,
                hasParent ? RandomData.GetRandomString(3, true, false, false, string.Empty) : null,
                Addresses.GenerateAddressDTO());
        }

        public static OrganisationDTO GetOrganisationDTOFromCreateDTO(CreateOrganisationDTO createDTO)
        {
            return new OrganisationDTO(
                Guid.NewGuid().ToString(),
                createDTO.Initials,
                createDTO.Name,
                createDTO.ParentId,
                string.IsNullOrEmpty(createDTO.ParentId)
                    ? null
                    : new Regex("[^A-Z]").Replace(Company.Name(), string.Empty),
                Addresses.GetAddressDTOFromCreateDTO(createDTO.Address));
        }

        public static OrganisationDTO GetOrganisationDTOFromUpdateDTO(UpdateOrganisationDTO updateDTO, Guid? organisationId = null)
        {
            organisationId ??= Guid.NewGuid();
            
            return new OrganisationDTO(
                organisationId.ToString(),
                updateDTO.Initials,
                updateDTO.Name,
                null,
                null,
                Addresses.GenerateAddressDTO());
        }

        public static List<PersonOrganisationDTO> GeneratePersonOrganisationDTOs(int numberToGenerate, Guid personId)
        {
            var personOrganisations = new List<PersonOrganisationDTO>();

            for (var i = 0; i < numberToGenerate; i++)
            {
                personOrganisations.Add(GeneratePersonOrganisationDTO(personId));
            }

            return personOrganisations;
        }

        public static PersonOrganisationDTO GeneratePersonOrganisationDTO(Guid personId)
        {
            return new PersonOrganisationDTO(People.GeneratePersonDTO(personId), GenerateOrganisationDTO(), false);
        }

        public static PersonOrganisationDTO GetPersonOrganisationDTOFromCreateDTO(CreatePersonOrganisationDTO createDTO)
        {
            return new PersonOrganisationDTO(
                People.GetPersonDTOFromCreateDTO(createDTO.Person),
                GetOrganisationDTOFromCreateDTO(createDTO.Organisation),
                false);
        }
    }
}