// <copyright file="Documents.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.MockData
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;

using MartialBase.API.Models.DTOs.Documents;
using MartialBase.Web.MockData.Tools;

namespace MartialBase.Web.MockData.DataGenerators;

public static class Documents
{
    public static DocumentDTO GenerateDocumentDTO(string? typeName = null)
    {
        typeName ??= DocumentTypes.GetRandomDocumentTypeName();

        var hasExpiry = RandomData.GetRandomBool();
        var documentId = Guid.NewGuid().ToString();

        return new DocumentDTO(
            documentId,
            Guid.NewGuid().ToString(),
            typeName,
            FakeData.Company.Name(),
            RandomData.GetRandomDate(DateTime.Now.AddMonths(-6), DateTime.Now),
            RandomData.GetRandomString(6, true, false),
            $"{FakeData.Internet.SecureUrl()}/{documentId}.pdf",
            hasExpiry ? RandomData.GetRandomDate(DateTime.Now, DateTime.Now.AddYears(2)) : null);
    }

    public static DocumentDTO GetDocumentDTOFromCreateDTO(CreateDocumentDTO createDTO, string? typeName = null)
    {
        typeName ??= DocumentTypes.GetRandomDocumentTypeName();
        var documentId = Guid.NewGuid().ToString();

        return new DocumentDTO(
            documentId,
            createDTO.DocumentTypeId,
            typeName,
            FakeData.Company.Name(),
            createDTO.DocumentDate,
            createDTO.Reference,
            createDTO.URL,
            createDTO.ExpiryDate);
    }
}