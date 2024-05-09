// <copyright file="MartialBaseUsers.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.DataGenerator
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

using MartialBase.API.Models.DTOs.MartialBaseUsers;

namespace MartialBase.Web.MockData.DataGenerators
{
    public static class MartialBaseUsers
    {
        public static List<MartialBaseUserDTO> GenerateMartialBaseUserDTOs(int numberToGenerate)
        {
            var users = new List<MartialBaseUserDTO>();

            for (var i = 0; i < numberToGenerate; i++)
            {
                users.Add(GenerateMartialBaseUserDTO());
            }

            return users;
        }
        
        public static MartialBaseUserDTO GenerateMartialBaseUserDTO()
        {
            return new MartialBaseUserDTO(Guid.NewGuid().ToString(), People.GeneratePersonDTO(), Guid.NewGuid().ToString(), null);
        }
    }
}