// <copyright file="IMartialBaseUserDataService.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.MartialBaseUsers;
using MartialBase.API.Models.DTOs.UserRoles;
using MartialBase.Web.Data.Utilities;

namespace MartialBase.Web.Data.Services.Interfaces
{
    public interface IMartialBaseUserDataService
    {
        Task<ApiResult<string>> GetInvitationCode(string userId, string token);

        Task<ApiResult<List<MartialBaseUserDTO>>> GetUsers(string token);

        Task<ApiResult<MartialBaseUserDTO>> GetUser(string userId, string token, bool? includePerson = null);

        Task<ApiResult<List<UserRoleDTO>>> GetRoles(string token);

        Task<ApiResult<List<UserRoleDTO>>> GetRolesForUser(string userId, string token);

        Task<ApiResult> AddRoleToUser(string userId, string roleId, string token);

        Task<ApiResult> RemoveRoleFromUser(string userId, string roleId, string token);

        Task<ApiResult> SetUserRoles(string userId, IEnumerable<Guid> userRoleIds, string token);

        Task<ApiResult> AssignPersonToUser(string userId, Guid personId, string token);
    }
}