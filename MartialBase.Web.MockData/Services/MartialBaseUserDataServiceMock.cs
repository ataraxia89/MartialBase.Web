// <copyright file="MartialBaseUserDataServiceMock.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.MartialBaseUsers;
using MartialBase.API.Models.DTOs.UserRoles;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;
using MartialBase.Web.MockData.DataGenerators;
using MartialBase.Web.MockData.Tools;

namespace MartialBase.Web.MockData.Services;

public class MartialBaseUserDataServiceMock : IMartialBaseUserDataService
{
    /// <inheritdoc />
    public async Task<ApiResult<string>> GetInvitationCode(string userId, string token)
    {
        var invitationCode = RandomData.GetRandomString(7, true, false);
        var response = HttpResponseGenerator.GetResponseMessage(invitationCode);

        return await ApiResult<string>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<List<MartialBaseUserDTO>>> GetUsers(string token)
    {
        var users = MartialBaseUsers.GenerateMartialBaseUserDTOs(20);
        var response = HttpResponseGenerator.GetResponseMessage(users);

        return await ApiResult<List<MartialBaseUserDTO>>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<MartialBaseUserDTO>> GetUser(string userId, string token, bool? includePerson = null)
    {
        var user = MartialBaseUsers.GenerateMartialBaseUserDTO();
        var response = HttpResponseGenerator.GetResponseMessage(user);

        return await ApiResult<MartialBaseUserDTO>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<List<UserRoleDTO>>> GetRoles(string token)
    {
        var userRoles = UserRoles.GetAllUserRoles();
        var response = HttpResponseGenerator.GetResponseMessage(userRoles);

        return await ApiResult<List<UserRoleDTO>>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult<List<UserRoleDTO>>> GetRolesForUser(string userId, string token)
    {
        var userRoles = UserRoles.GetAllUserRoles();
        var response = HttpResponseGenerator.GetResponseMessage(userRoles);

        return await ApiResult<List<UserRoleDTO>>.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> AddRoleToUser(string userId, string roleId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.Created);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> RemoveRoleFromUser(string userId, string roleId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.NoContent);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> SetUserRoles(string userId, IEnumerable<Guid> userRoleIds, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK);

        return await ApiResult.GenerateAPIResult(response);
    }

    /// <inheritdoc />
    public async Task<ApiResult> AssignPersonToUser(string userId, Guid personId, string token)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK);

        return await ApiResult.GenerateAPIResult(response);
    }
}