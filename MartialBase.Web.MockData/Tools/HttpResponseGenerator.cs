// <copyright file="HttpResponseGenerator.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.DataGenerator
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace MartialBase.Web.MockData.Tools
{
    public static class HttpResponseGenerator
    {
        public static HttpResponseMessage GetResponseMessage(object content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonSerializer.Serialize(content))
            };
        }
    }
}