// <copyright file="People.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.DataGenerator
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

using MartialBase.API.Models.DTOs.People;
using MartialBase.FakeData;
using MartialBase.Web.MockData.Tools;

namespace MartialBase.Web.MockData.DataGenerators
{
    public static class People
    {
        public static List<PersonDTO> GeneratePersonDTOs(
            int numberToGenerate,
            string? email = null,
            string? firstName = null,
            string? middleName = null,
            string? lastName = null)
        {
            var people = new List<PersonDTO>();

            for (var i = 0; i < numberToGenerate; i++)
            {
                people.Add(GeneratePersonDTO(null, email, firstName, middleName, lastName));
            }

            return people;
        }
        
        public static PersonDTO GeneratePersonDTO(
            Guid? personId = null,
            string? email = null,
            string? firstName = null,
            string? middleName = null,
            string? lastName = null)
        {
            var rnd = RandomData.GetRandomNumber(0, 1);
            var fullName = rnd == 0
                ? Name.FullNameMale(NameFormats.StandardWithMiddleWithPrefix)
                : Name.FullNameFemale(NameFormats.StandardWithMiddleWithPrefix);

            var person = new PersonDTO(
                personId ?? Guid.NewGuid(),
                null,
                null,
                null,
                null,
                DateTime.Now.AddYears(-25),
                Addresses.GenerateAddressDTO(),
                Phone.Number(),
                null);

            SetPersonName(person, fullName, true);

            if (firstName != null)
            {
                person.FirstName = $"{firstName[0].ToString().ToUpper()}{firstName[1..]}";
            }

            if (middleName != null)
            {
                person.MiddleName = $"{middleName[0].ToString().ToUpper()}{middleName[1..]}";
            }

            if (lastName != null)
            {
                person.LastName = $"{lastName[0].ToString().ToUpper()}{lastName[1..]}";
            }

            person.Email = email ?? Internet.Email($"{person.FirstName} {person.LastName}");

            return person;
        }

        public static void SetPersonName(PersonDTO person, string fullName, bool includesTitle = false)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            if (fullName == null)
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            var personNames = fullName.Split(' ');

            if (includesTitle)
            {
                person.Title = personNames[0];

                personNames = fullName.Replace(person.Title, string.Empty).Trim().Split(' ');
            }

            switch (personNames.Length)
            {
                case 1:
                    person.FirstName = personNames[0];
                    person.MiddleName = null;
                    person.LastName = null;
                    break;
                case 2:
                    person.FirstName = personNames[0];
                    person.MiddleName = null;
                    person.LastName = personNames[1];
                    break;
                default:
                    person.FirstName = personNames[0];
                    person.MiddleName = string.Join(' ', personNames.Skip(1).Take(personNames.Length - 2));
                    person.LastName = personNames[^1];
                    break;
            }
        }

        public static List<OrganisationPersonDTO> GenerateOrganisationPersonDTOs(int numberToGenerate)
        {
            var organisationPeople = new List<OrganisationPersonDTO>();

            for (var i = 0; i < numberToGenerate; i++)
            {
                organisationPeople.Add(GenerateOrganisationPersonDTO());
            }

            return organisationPeople;
        }

        public static OrganisationPersonDTO GenerateOrganisationPersonDTO()
        {
            return new OrganisationPersonDTO(GeneratePersonDTO(), false);
        }

        public static List<SchoolStudentDTO> GenerateSchoolStudentDTOs(int numberToGenerate, Guid? schoolId = null)
        {
            var schoolStudents = new List<SchoolStudentDTO>();

            for (var i = 0; i < numberToGenerate; i++)
            {
                schoolStudents.Add(GenerateSchoolStudentDTO(schoolId));
            }

            return schoolStudents;
        }

        public static SchoolStudentDTO GenerateSchoolStudentDTO(Guid? schoolId = null)
        {
            schoolId ??= Guid.NewGuid();

            return new SchoolStudentDTO(
                GeneratePersonDTO(),
                (Guid)schoolId,
                Company.Name(),
                Documents.GenerateDocumentDTO("Insurance Certificate"),
                Documents.GenerateDocumentDTO("Licence Certificate"),
                false,
                false);
        }

        public static PersonDTO GetPersonDTOFromCreateDTO(CreatePersonDTO createDTO)
        {
            return new PersonDTO(
                Guid.NewGuid(),
                createDTO.Title,
                createDTO.FirstName,
                createDTO.MiddleName,
                createDTO.LastName,
                DateTime.Parse(createDTO.DateOfBirth),
                Addresses.GetAddressDTOFromCreateDTO(createDTO.Address),
                createDTO.MobileNo,
                createDTO.Email);
        }

        public static PersonDTO GetPersonDTOFromUpdateDTO(Guid personId, UpdatePersonDTO updateDTO)
        {
            return new PersonDTO(
                personId,
                updateDTO.Title,
                updateDTO.FirstName,
                updateDTO.MiddleName,
                updateDTO.LastName,
                RandomData.GetRandomDate(DateTime.Now.AddYears(-50), DateTime.Now.AddYears(-18)),
                Addresses.GetAddressDTOFromUpdateDTO(updateDTO.Address),
                updateDTO.MobileNo,
                updateDTO.Email);
        }
    }
}