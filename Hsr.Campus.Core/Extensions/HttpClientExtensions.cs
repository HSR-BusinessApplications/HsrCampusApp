// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace System.Net
{
    using Http;
    using Threading;
    using Threading.Tasks;

    public static class HttpClientExtensions
    {
        /// <summary>
        /// Sends an asynchronous GET- request to a specifig URI and returns the content of the answer
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> with which the request will be processed</param>
        /// <param name="requestUri">Destination URI</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to cancel the operation</param>
        /// <returns>Content of the HttpResponseMessage</returns>
        /// <exception cref="HttpRequestException">Is thrown when the HttpRequest has failed</exception>
        public static async Task<string> GetStringAsync(this HttpClient client, string requestUri, CancellationToken cancellationToken)
        {
            var response = await client.GetAsync(new Uri(requestUri), cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.StatusCode.ToString());
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
