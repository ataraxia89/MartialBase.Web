// <copyright file="UserRoles.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.DataGenerator
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

using MartialBase.API.Models.DTOs.UserRoles;

namespace MartialBase.Web.MockData.DataGenerators
{
    public static class UserRoles
    {
        public static List<UserRoleDTO> GetAllUserRoles()
        {
            var userRoles = new List<UserRoleDTO>();
            var roles = API.Models.Collections.UserRole.GetRoles;

            foreach (var role in roles)
            {
                userRoles.Add(new UserRoleDTO(Guid.NewGuid().ToString(), role));
            }

            return userRoles;
        }
    }
}