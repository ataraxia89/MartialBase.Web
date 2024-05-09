// <copyright file="MartialBaseUserDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.MartialBaseUsers;
using MartialBase.API.Models.DTOs.UserRoles;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;

namespace MartialBase.Web.Data.Services
{
    public class MartialBaseUserDataService : IMartialBaseUserDataService
    {
        /// <inheritdoc />
        public async Task<ApiResult<string>> GetInvitationCode(string userId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "admin", "users", userId, "invitationcode" },
                token);

            return await ApiResult<string>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<List<MartialBaseUserDTO>>> GetUsers(string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "admin", "users" },
                token);

            return await ApiResult<List<MartialBaseUserDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<MartialBaseUserDTO>> GetUser(string userId, string token, bool? includePerson = null)
        {
            var queryParameters = new Dictionary<string, string>();

            if (includePerson != null)
            {
                queryParameters.Add("includePerson", includePerson.ToString().ToLower());
            }

            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "admin", "users", userId },
                queryParameters,
                token);

            return await ApiResult<MartialBaseUserDTO>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<List<UserRoleDTO>>> GetRoles(string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "admin", "roles" },
                token);

            return await ApiResult<List<UserRoleDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult<List<UserRoleDTO>>> GetRolesForUser(string userId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Get,
                new[] { "admin", "users", userId, "roles" },
                token);

            return await ApiResult<List<UserRoleDTO>>.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> AddRoleToUser(string userId, string roleId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Post,
                new[] { "admin", "users", userId, "roles" },
                new Dictionary<string, string> { { "roleId", roleId } },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> RemoveRoleFromUser(string userId, string roleId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Delete,
                new[] { "admin", "users", userId, "roles", roleId },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> SetUserRoles(string userId, IEnumerable<Guid> userRoleIds, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Put,
                new[] { "admin", "users", userId, "roles" },
                userRoleIds,
                token);

            return await ApiResult.GenerateAPIResult(response);
        }

        /// <inheritdoc />
        public async Task<ApiResult> AssignPersonToUser(string userId, Guid personId, string token)
        {
            HttpResponseMessage response = await JsonRequestHelper.GetResponse(
                HttpMethod.Put,
                new[] { "admin", "users", userId, "person" },
                new Dictionary<string, string> { { "personId", personId.ToString() } },
                token);

            return await ApiResult.GenerateAPIResult(response);
        }
    }
}