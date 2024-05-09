// <copyright file="ApiResult.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using MartialBase.API.Models.Enums;
using Newtonsoft.Json;

namespace MartialBase.Web.Data.Utilities
{
    /// <summary>
    /// A helper class to return the result of an API call including a returned object.
    /// </summary>
    /// <typeparam name="T">The object type to be returned.</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Needs to be common naming.")]
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResult{T}"/> class.
        /// </summary>
        /// <remarks>If <paramref name="errorResponseCode"/> is <see cref="ErrorResponseCode.None"/>, <see cref="ApiResult.IsSuccess"/> will be set to true, otherwise false.</remarks>
        /// <param name="returnObject">The response object to be returned.</param>
        /// <param name="errorResponseCode">The error response code parsed from the API response. If this is set as <see cref="MartialBase.API.Models.Enums.ErrorResponseCode.None"/>, <see cref="ApiResult.IsSuccess"/> will be set as true, otherwise false.</param>
        public ApiResult(T returnObject, ErrorResponseCode errorResponseCode = ErrorResponseCode.None)
            : base(errorResponseCode)
        {
            Object = returnObject;
        }

        /// <summary>
        /// Gets the object returned in the API response.
        /// </summary>
        public T Object { get; }

        public new static async Task<ApiResult<T>> GenerateAPIResult(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ErrorResponseCode errorResponseCode = ResponseCodeHandler.GetErrorResponseCode(content);

#pragma warning disable CS8604 // Possible null reference argument.
                return new ApiResult<T>(default, errorResponseCode);
#pragma warning restore CS8604 // Possible null reference argument.
            }

            T returnObject = typeof(T) == typeof(string) ? (T)Convert.ChangeType(content, typeof(T)) : JsonConvert.DeserializeObject<T>(content);

            return new ApiResult<T>(returnObject);
        }
    }

    /// <summary>
    /// A helper class to return the result of an API call.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Needs to be common naming.")]
    public class ApiResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResult"/> class.
        /// </summary>
        /// <remarks>If <paramref name="errorResponseCode"/> is <see cref="MartialBase.API.Models.Enums.ErrorResponseCode.None"/>, <see cref="IsSuccess"/> will be set to true, otherwise false.</remarks>
        /// <param name="errorResponseCode">The error response code parsed from the API response. If this is set as <see cref="MartialBase.API.Models.Enums.ErrorResponseCode.None"/>, <see cref="IsSuccess"/> will be set as true, otherwise false.</param>
        public ApiResult(ErrorResponseCode errorResponseCode)
        {
            ErrorResponseCode = errorResponseCode;
            IsSuccess = ErrorResponseCode == ErrorResponseCode.None;
        }

        /// <summary>
        /// Gets the error response code returned in the API response.
        /// </summary>
        public ErrorResponseCode ErrorResponseCode { get; }

        /// <summary>
        /// Gets a value indicating whether the API response was a successful status code.
        /// </summary>
        public bool IsSuccess { get; }

        public static async Task<ApiResult> GenerateAPIResult(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? new ApiResult(ErrorResponseCode.None)
                : new ApiResult(ResponseCodeHandler.GetErrorResponseCode(content));
        }
    }
}