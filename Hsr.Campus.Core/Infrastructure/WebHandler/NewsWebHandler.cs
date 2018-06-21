// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.WebHandler
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using Newtonsoft.Json;

    public class NewsWebHandler : INewsWebHandler
    {
        private readonly IDevice device;
        private readonly IServiceApi serviceApi;
        private readonly IHttpClientConfiguration httpClientConfiguration;

        public NewsWebHandler(IDevice device, IServiceApi serviceApi, IHttpClientConfiguration httpClientConfiguration)
        {
            this.device = device;
            this.serviceApi = serviceApi;
            this.httpClientConfiguration = httpClientConfiguration;
        }

        public async Task<IEnumerable<WhNewsFeed>> GetFeedsAsync(CancellationToken cancellationToken)
        {
            var response = await this.GetJsonFeedsAsync(cancellationToken);

            return cancellationToken.IsCancellationRequested ? null : JsonConvert.DeserializeObject<IEnumerable<WhNewsFeed>>(response);
        }

        public Task<byte[]> GetFeedIconAsync(SqlNewsFeed feed, int size) => this.GetByteArrayIconAsync(feed, size);

        public async Task<IEnumerable<WhNews>> GetNewsEntriesAsync(SqlNewsFeed feed, DateTime before, CancellationToken cancellationToken)
        {
            var response = await this.GetJsonNewsEntriesAsync(feed, before, cancellationToken);

            return cancellationToken.IsCancellationRequested ? null : JsonConvert.DeserializeObject<IEnumerable<WhNews>>(response);
        }

        public string PictureUrl(string newsId) => string.Format(this.serviceApi.PictureUri, newsId);

        protected virtual Task<string> GetJsonFeedsAsync(CancellationToken cancellationToken)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignAcceptTypeJson()
                .AssignAuthToken(this.serviceApi.News);

            return webClient.GetStringAsync(this.serviceApi.NewsFeedsUri, cancellationToken);
        }

        protected virtual Task<byte[]> GetByteArrayIconAsync(SqlNewsFeed feed, int size)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignAcceptTypeJson()
                .AssignAuthToken(this.serviceApi.News);

            return webClient.GetByteArrayAsync(string.Format(this.serviceApi.IconUri, feed.Key, size));
        }

        /// <summary>
        /// Returns the raw JSON data from the NewsFeed-Service
        /// </summary>
        /// <param name="feed">Name of the feed</param>
        /// <param name="before">Maximum date of the newest entry which will be returned</param>
        /// <param name="cancellationToken">Token which can be used to cancel the process</param>
        /// <returns>JSON-String with the news entries</returns>
        protected virtual Task<string> GetJsonNewsEntriesAsync(SqlNewsFeed feed, DateTime before, CancellationToken cancellationToken)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignAcceptTypeJson()
                .AssignAuthToken(this.serviceApi.News);

            return webClient.GetStringAsync(string.Format(this.serviceApi.NewsUri, feed.Key, before), cancellationToken);
        }
    }
}
