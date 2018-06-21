// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.Sync
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Model;

    public class NewsSync : INewsSync
    {
        private readonly IDevice device;
        private readonly ILogging logging;
        private readonly INewsWebHandler handler;
        private readonly INewsRepository repo;

        private readonly TimeSpan updatePauseTime = new TimeSpan(0, 15, 0);

        public NewsSync(INewsWebHandler handler, INewsRepository repo, IDevice device, ILogging logging)
        {
            this.handler = handler;
            this.repo = repo;
            this.device = device;
            this.logging = logging;
        }

        public async Task<ResultState> UpdateFeedsAsync(bool force, CancellationToken cancellationToken)
        {
            if (!force && this.repo.LastUpdated > DateTime.UtcNow.Subtract(this.updatePauseTime))
            {
                return ResultState.NotModified;
            }

            if (!this.device.HasNetworkConnectivity)
            {
                return ResultState.ErrorNetwork;
            }

            try
            {
                var feeds = await this.handler.GetFeedsAsync(cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    return ResultState.Canceled;
                }

                if (feeds == null || !feeds.Any())
                {
                    this.repo.Truncate();
                    return ResultState.NoData;
                }

                this.repo.UpdateFeeds(feeds);

                // Only iOS uses Icons
                if (this.device.Platform != DevicePlatform.iOS)
                {
                    return ResultState.Success;
                }

                foreach (var feed in this.repo.RetrieveFeeds())
                {
                    if (feed.LastIconUpdate > DateTime.UtcNow.Date.AddDays(-5))
                    {
                        continue;
                    }

                    var iconBytes = await this.handler.GetFeedIconAsync(feed, this.device.TapIconSize);

                    if (iconBytes != null && iconBytes.Any())
                    {
                        this.repo.UpdateIcon(feed, iconBytes, this.device.TapIconSize);
                    }
                }

                return ResultState.Success;
            }
            catch (Exception ex)
            {
                // most likely network connectivity problems
                this.logging.Exception(this, ex);
                return ResultState.Error;
            }
        }

        public async Task<Tuple<ResultState, IEnumerable<SqlNews>>> UpdateNewsAsync(bool force, SqlNewsFeed feed, DateTime before, CancellationToken cancellationToken)
        {
            if (!this.device.HasNetworkConnectivity)
            {
                return new Tuple<ResultState, IEnumerable<SqlNews>>(ResultState.ErrorNetwork, null);
            }

            var storedPosts = this.repo.RetrieveNewsBefore(feed, before, 10);
            if (!force && storedPosts.Any() && storedPosts.All(news => news.LastUpdated > DateTime.UtcNow.Subtract(this.updatePauseTime)))
            {
                return new Tuple<ResultState, IEnumerable<SqlNews>>(ResultState.NotModified, null);
            }

            try
            {
                var news = await this.handler.GetNewsEntriesAsync(feed, before, cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    return new Tuple<ResultState, IEnumerable<SqlNews>>(ResultState.Canceled, null);
                }

                if (news == null || !news.Any())
                {
                    if (force)
                    {
                        this.repo.DeleteNewsEntries(feed);
                    }

                    return new Tuple<ResultState, IEnumerable<SqlNews>>(ResultState.NoData, null);
                }

                var result = this.repo.UpdateNewsRange(feed, news);

                return new Tuple<ResultState, IEnumerable<SqlNews>>(ResultState.Success, result);
            }
            catch (Exception ex)
            {
                // most likely network connectivity problems
                this.logging.Exception(this, ex);
                return new Tuple<ResultState, IEnumerable<SqlNews>>(ResultState.Error, null);
            }
        }
    }
}
