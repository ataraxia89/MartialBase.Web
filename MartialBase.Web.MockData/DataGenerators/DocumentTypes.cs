// <copyright file="DocumentTypes.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.DataGenerator
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

using MartialBase.API.Models.DTOs.DocumentTypes;
using MartialBase.Web.MockData.Tools;

namespace MartialBase.Web.MockData.DataGenerators
{
    public static class DocumentTypes
    {
        private static readonly List<string> DocumentTypeNames = new()
        {
            "Licence Certificate",
            "Insurance Certificate",
            "Application Form",
            "Order Form",
            "Registration Form",
            "Membership Renewal Form",
            "Delivery Note",
            "Invoice"
        };
        
        public static List<DocumentTypeDTO> GenerateDocumentTypeDTOs(int numberToGenerate, Guid? organisationId = null)
        {
            var documentTypeDTOs = new List<DocumentTypeDTO>();

            for (var i = 0; i < numberToGenerate; i++)
            {
                documentTypeDTOs.Add(GenerateDocumentTypeDTO(organisationId));
            }

            return documentTypeDTOs;
        }

        public static DocumentTypeDTO GenerateDocumentTypeDTO(Guid? organisationId = null)
        {
            var companyName = organisationId != null ? FakeData.Company.Name() : null;
            var hasExpiry = RandomData.GetRandomBool();
            
            organisationId ??= Guid.NewGuid();
            
            return new DocumentTypeDTO(
                Guid.NewGuid().ToString(),
                organisationId.ToString(),
                companyName ?? FakeData.Company.Name(),
                RandomData.GetRandomString(5),
                GetRandomDocumentTypeName(),
                hasExpiry ? RandomData.GetRandomNumber(0, 1000) : null,
                FakeData.Internet.SecureUrl());
        }

        public static string GetRandomDocumentTypeName()
        {
            return DocumentTypeNames[RandomData.GetRandomNumber(0, DocumentTypeNames.Count - 1)];
        }
    }
}