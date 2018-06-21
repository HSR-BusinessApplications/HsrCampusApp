// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace System.Net
{
    using Hsr.Campus.Core.ApplicationServices;
    using Hsr.Campus.Core.Properties;
    using Http;
    using Http.Headers;

    public static class HttpWebRequestExtensions
    {
        /// <summary>
        /// Assigns this App's User-agent HTTP Header
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> whose Agent header should be processed</param>
        /// <param name="device"><see cref="IDevice"/> with data that should be set as the agent</param>
        /// <returns>A HttpWebRequest with the App's User-agent.</returns>
        public static HttpClient AssignAgent(this HttpClient client, IDevice device)
        {
            client.DefaultRequestHeaders.UserAgent.Clear();

            const string userAgentProduct = "HsrCampus/" + AssemblyInfo.AssemblyInformationalVersion;
            string userAgentComment = $"Xamarin; {device.Platform} {device.Version}; {device.Model}";

            try
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    "User-Agent",
                    $"{userAgentProduct} ({userAgentComment})");
            }
            catch (Exception)
            {
                // ignore
                return client;
            }

            return client;
        }

        /// <summary>
        /// Assigns a http header to the request
        /// </summary>
        /// <param name="client">The <see cref="HttpClient"/> to modify</param>
        /// <param name="headerName">Header name</param>
        /// <param name="value">Header value</param>
        /// <returns>A <see cref="HttpClient"/> with the assigned header</returns>
        public static HttpClient AssignHeader(this HttpClient client, string headerName, string value)
        {
            client.DefaultRequestHeaders.Add(headerName, value);
            return client;
        }

        public static HttpClient AssignAuthToken(this HttpClient client, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("token cannot be null or empty.");
            }

            client.DefaultRequestHeaders.Add("X-Auth", token);

            return client;
        }

        public static HttpClient AssignBearerToken(this HttpClient client, string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new InvalidOperationException("accessToken cannot be null or empty.");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return client;
        }

        public static HttpClient AssignContentType(this HttpClient client, string contentType)
        {
            client.DefaultRequestHeaders.Add("ContentType", contentType);
            return client;
        }

        public static HttpClient AssignContentTypeJson(this HttpClient client) => AssignContentType(client, "application/json");

        public static HttpClient AssignAcceptTypeJson(this HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static HttpClient AssignAcceptType(this HttpClient client, string contentType)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            return client;
        }
    }
}
