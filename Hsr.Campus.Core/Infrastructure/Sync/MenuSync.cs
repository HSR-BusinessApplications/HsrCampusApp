// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.Sync
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Model;

    public class MenuSync : IMenuSync
    {
        private readonly IMenuWebHandler handler;
        private readonly IMenuRepository repo;
        private readonly IDevice device;
        private readonly ILogging logging;

        private readonly TimeSpan updatePauseTime = new TimeSpan(1, 0, 0);

        public MenuSync(IMenuWebHandler handler, IMenuRepository repo, IDevice device, ILogging logging)
        {
            this.handler = handler;
            this.repo = repo;
            this.device = device;
            this.logging = logging;
        }

        public static string GenerateMenuId(WhMenuFeed feed, DateTime day)
        {
            return $"{feed.Id}_{day:dd-MM-yy}";
        }

        public async Task<ResultState> UpdateAsync(bool force, CancellationToken cancellationToken)
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

                var marker = this.repo.MarkMenus();

                foreach (var feed in feeds)
                {
                    foreach (var day in feed.Days)
                    {
                        var id = GenerateMenuId(feed, day.Date);

                        if (!force && this.repo.RetrieveMenu(id)?.ContentHash == day.ContentHash)
                        {
                            this.repo.UnmarkMenu(id);
                            continue;
                        }

                        var menuHtml = await this.handler.GetMenuHtmlAsync(feed.Id, day.Date, cancellationToken);

                        if (cancellationToken.IsCancellationRequested)
                        {
                            return ResultState.Canceled;
                        }

                        if (!string.IsNullOrEmpty(menuHtml))
                        {
                            this.repo.UpdateMenu(id, feed.Id, day, menuHtml);
                        }
                    }
                }

                this.repo.DeleteMarkedMenus(marker);

                return ResultState.Success;
            }
            catch (Exception ex)
            {
                // most likely network connectivity problems
                this.logging.Exception(this, ex);
                return ResultState.Error;
            }
        }
    }
}
