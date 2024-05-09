// <copyright file="ResponseCodeHandler.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;

using MartialBase.API.Models.Enums;

namespace MartialBase.Web.Data.Utilities
{
    public static class ResponseCodeHandler
    {
        public static ErrorResponseCode GetErrorResponseCode(string content)
        {
            if (int.TryParse(content, out int responseCode) &&
                Enum.IsDefined(typeof(ErrorResponseCode), responseCode))
            {
                return (ErrorResponseCode)responseCode;
            }

            return ErrorResponseCode.UnknownError;
        }
    }
}