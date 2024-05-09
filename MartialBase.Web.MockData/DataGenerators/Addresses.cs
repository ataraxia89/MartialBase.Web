// <copyright file="Addresses.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.DataGenerator
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;

using MartialBase.API.Models.DTOs.Addresses;
using MartialBase.FakeData;

namespace MartialBase.Web.MockData.DataGenerators
{
    public static class Addresses
    {
        public static AddressDTO GenerateAddressDTO()
        {
            return new AddressDTO
            {
                Id = Guid.NewGuid().ToString(),
                Line1 = Address.StreetAddress(),
                Line2 = Address.SecondaryAddress(),
                Line3 = Address.SecondaryAddress(),
                Town = Address.City(),
                County = Address.UkCounty(),
                PostCode = Address.UkPostCode().ToUpper(),
                CountryCode = "UK",
                LandlinePhone = Phone.Number(),
                CountryName = "United Kingdom"
            };
        }

        public static AddressDTO GetAddressDTOFromCreateDTO(CreateAddressDTO createDTO)
        {
            return new AddressDTO
            {
                Id = Guid.NewGuid().ToString(),
                Line1 = createDTO.Line1,
                Line2 = createDTO.Line2,
                Line3 = createDTO.Line3,
                Town = createDTO.Town,
                County = createDTO.County,
                PostCode = createDTO.PostCode,
                CountryCode = createDTO.CountryCode,
                LandlinePhone = createDTO.LandlinePhone,
                CountryName = createDTO.CountryCode
            };
        }

        public static AddressDTO GetAddressDTOFromUpdateDTO(UpdateAddressDTO updateDTO)
        {
            return new AddressDTO
            {
                Id = Guid.NewGuid().ToString(),
                Line1 = updateDTO.Line1,
                Line2 = updateDTO.Line2,
                Line3 = updateDTO.Line3,
                Town = updateDTO.Town,
                County = updateDTO.County,
                PostCode = updateDTO.PostCode,
                CountryCode = updateDTO.CountryCode,
                LandlinePhone = updateDTO.LandlinePhone,
                CountryName = updateDTO.CountryCode
            };
        }
    }
}