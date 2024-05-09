// <copyright file="Schools.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.DataGenerator
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

using MartialBase.API.Models.DTOs.Addresses;
using MartialBase.API.Models.DTOs.People;
using MartialBase.API.Models.DTOs.Schools;

namespace MartialBase.Web.MockData.DataGenerators
{
    public static class Schools
    {
        public static List<SchoolDTO> GenerateSchoolDTOs(int numberToGenerate, Guid? artId = null, Guid? organisationId = null)
        {
            var schools = new List<SchoolDTO>();

            for (var i = 0; i < numberToGenerate; i++)
            {
                schools.Add(GenerateSchoolDTO(artId, organisationId));
            }

            return schools;
        }

        public static SchoolDTO GenerateSchoolDTO(Guid? artId = null, Guid? organisationId = null, Guid? schoolId = null)
        {
            var schoolAddress = Addresses.GenerateAddressDTO();

            artId ??= Guid.NewGuid();
            organisationId ??= Guid.NewGuid();
            schoolId ??= Guid.NewGuid();

            return new SchoolDTO(
                (Guid)schoolId,
                artId.ToString(),
                Arts.GetRandomArtName(),
                organisationId.ToString(),
                FakeData.Company.Name(),
                FakeData.Company.Name(),
                Guid.NewGuid().ToString(),
                FakeData.Name.FullName(),
                schoolAddress,
                FakeData.Phone.Number(),
                FakeData.Internet.Email(),
                FakeData.Internet.SecureUrl(),
                new List<AddressDTO> { schoolAddress, Addresses.GenerateAddressDTO() });
        }

        public static List<StudentSchoolDTO> GenerateStudentSchoolDTOs(int numberToGenerate)
        {
            var studentSchools = new List<StudentSchoolDTO>();

            for (var i = 0; i < numberToGenerate; i++)
            {
                studentSchools.Add(GenerateStudentSchoolDTO());
            }

            return studentSchools;
        }

        public static StudentSchoolDTO GenerateStudentSchoolDTO()
        {
            return new StudentSchoolDTO(
                GenerateSchoolDTO(),
                Documents.GenerateDocumentDTO("Insurance Certificate"),
                Documents.GenerateDocumentDTO("Licence Certificate"),
                false,
                false);
        }

        public static SchoolDTO GetSchoolDTOFromCreateDTO(CreateSchoolDTO createDTO)
        {
            var schoolAddress = Addresses.GetAddressDTOFromCreateDTO(createDTO.Address);
            var trainingVenues = new List<AddressDTO> { schoolAddress };

            foreach (var address in createDTO.AdditionalTrainingVenues)
            {
                trainingVenues.Add(Addresses.GetAddressDTOFromCreateDTO(address));
            }

            return new SchoolDTO(
                Guid.NewGuid(),
                createDTO.ArtId,
                Arts.GetRandomArtName(),
                createDTO.OrganisationId,
                FakeData.Company.Name(),
                createDTO.Name,
                createDTO.HeadInstructorId,
                FakeData.Name.FullName(),
                schoolAddress,
                createDTO.PhoneNo,
                createDTO.EmailAddress,
                createDTO.WebsiteURL,
                trainingVenues);
        }

        public static SchoolDTO GetSchoolDTOFromUpdateDTO(Guid schoolId, UpdateSchoolDTO updateDTO)
        {
            var schoolAddress = Addresses.GetAddressDTOFromUpdateDTO(updateDTO.Address);
            var trainingVenues = new List<AddressDTO> { schoolAddress };

            foreach (var address in updateDTO.AdditionalTrainingVenues)
            {
                trainingVenues.Add(Addresses.GetAddressDTOFromUpdateDTO(address.Value));
            }

            return new SchoolDTO(
                schoolId,
                Guid.NewGuid().ToString(),
                Arts.GetRandomArtName(),
                Guid.NewGuid().ToString(),
                FakeData.Company.Name(),
                updateDTO.Name,
                Guid.NewGuid().ToString(),
                FakeData.Name.FullName(),
                schoolAddress,
                updateDTO.PhoneNo,
                updateDTO.EmailAddress,
                updateDTO.WebsiteURL,
                trainingVenues);
        }
    }
}