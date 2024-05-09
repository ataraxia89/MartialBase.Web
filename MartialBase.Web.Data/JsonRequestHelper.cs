// <copyright file="JsonRequestHelper.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace MartialBase.Web.Data
{
    /// <summary>
    /// A helper class designed to construct HTTP requests and handle responses.
    /// </summary>
    public static class JsonRequestHelper
    {
        /// <summary>
        /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
        /// </summary>
        /// <param name="method">The HTTP method used for the request.</param>
        /// <param name="endpoint">The endpoint to send the HTTP request to.</param>
        /// <param name="authToken">The authentication bearer token to be included in the request headers.</param>
        /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
        public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint, string authToken)
        {
            return await GetResponse(method, new[] { endpoint }, new Dictionary<string, string>(), null, authToken);
        }

        /// <summary>
        /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
        /// </summary>
        /// <param name="method">The HTTP method used for the request.</param>
        /// <param name="endpointRoute">The endpoint route to send the HTTP request to.</param>
        /// <param name="authToken">The authentication bearer token to be included in the request headers.</param>
        /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
        public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string[] endpointRoute, string authToken)
        {
            return await GetResponse(method, endpointRoute, new Dictionary<string, string>(), null, authToken);
        }

        /// <summary>
        /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
        /// </summary>
        /// <param name="method">The HTTP method used for the request.</param>
        /// <param name="endpoint">The endpoint to send the HTTP request to.</param>
        /// <param name="jsonObject">The object to be included in the request body.</param>
        /// <param name="authToken">The authentication bearer token to be included in the request headers.</param>
        /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
        public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint, object jsonObject, string authToken)
        {
            return await GetResponse(method, new[] { endpoint }, new Dictionary<string, string>(), jsonObject, authToken);
        }

        /// <summary>
        /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
        /// </summary>
        /// <param name="method">The HTTP method used for the request.</param>
        /// <param name="endpointRoute">The endpoint route to send the HTTP request to.</param>
        /// <param name="jsonObject">The object to be included in the request body.</param>
        /// <param name="authToken">The authentication bearer token to be included in the request headers.</param>
        /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
        public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string[] endpointRoute, object jsonObject, string authToken)
        {
            return await GetResponse(method, endpointRoute, new Dictionary<string, string>(), jsonObject, authToken);
        }

        /// <summary>
        /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
        /// </summary>
        /// <param name="method">The HTTP method used for the request.</param>
        /// <param name="endpoint">The endpoint to send the HTTP request to.</param>
        /// <param name="parameters">The list of parameters to be sent to the endpoint.</param>
        /// <param name="authToken">The authentication bearer token to be included in the request headers.</param>
        /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
        public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint, Dictionary<string, string> parameters, string authToken)
        {
            return await GetResponse(method, new[] { endpoint }, parameters, null, authToken);
        }

        /// <summary>
        /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
        /// </summary>
        /// <param name="method">The HTTP method used for the request.</param>
        /// <param name="endpointRoute">The endpoint route to send the HTTP request to.</param>
        /// <param name="parameters">The list of parameters to be sent to the endpoint.</param>
        /// <param name="authToken">The authentication bearer token to be included in the request headers.</param>
        /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
        public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string[] endpointRoute, Dictionary<string, string> parameters, string authToken)
        {
            return await GetResponse(method, endpointRoute, parameters, null, authToken);
        }

        /// <summary>
        /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
        /// </summary>
        /// <param name="method">The HTTP method used for the request.</param>
        /// <param name="endpoint">The endpoint to send the HTTP request to.</param>
        /// <param name="parameters">The list of parameters to be sent to the endpoint.</param>
        /// <param name="jsonObject">The object to be included in the request body.</param>
        /// <param name="authToken">The authentication bearer token to be included in the request headers.</param>
        /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
        public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint, Dictionary<string, string> parameters, object jsonObject, string authToken)
        {
            return await GetResponse(method, new[] { endpoint }, parameters, jsonObject, authToken);
        }

        /// <summary>
        /// Constructs and sends an HTTP request to the API.
        /// </summary>
        /// <param name="method">The HTTP method used for the request.</param>
        /// <param name="endpointRoute">The endpoint route to send the HTTP request to.</param>
        /// <param name="parameters">The list of parameters to be sent to the endpoint.</param>
        /// <param name="jsonObject">The object to be included in the request body.</param>
        /// <param name="authToken">The authentication bearer token to be included in the request headers.</param>
        /// <returns>A <see cref="HttpResponseMessage"/> result.</returns>
        /// <exception cref="NotImplementedException">When an unsupported <see cref="HttpAction"/> is provided.</exception>
        public static async Task<HttpResponseMessage> GetResponse(
            HttpMethod method,
            string[] endpointRoute,
            Dictionary<string, string> parameters,
            object jsonObject,
            string authToken)
        {
            if ((method == HttpMethod.Get || method == HttpMethod.Delete) && jsonObject != null)
            {
                throw new WebException("JSON bodies are not allowed for GET or DELETE requests.");
            }

            string apiAddr = AppData.APIAddress;
            string webAddr = apiAddr + string.Join("/", endpointRoute);

            if (parameters.Any())
            {
                string[] parameterArray = parameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))
                    .ToArray();

                webAddr += $"?{string.Join("&", parameterArray)}";
            }

            string requestBody = string.Empty;

            if (jsonObject != null)
            {
                requestBody = JsonConvert.SerializeObject(jsonObject);
            }

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(method, webAddr);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(authToken))
            {
                request.Headers.Add("Authorization", $"Bearer {authToken}");
            }

            return await client.SendAsync(request);
        }
    }
}