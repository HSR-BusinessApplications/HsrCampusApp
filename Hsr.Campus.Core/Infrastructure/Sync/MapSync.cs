// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.Sync
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Model;

    public class MapSync : IMapSync
    {
        private readonly IDevice device;
        private readonly ILogging logging;

        private readonly IMapWebHandler webHandler;
        private readonly IMapRepository repo;

        private readonly IIOCacheService cache;

        public MapSync(IMapWebHandler webHandler, IMapRepository repo, IDevice device, ILogging logging, IIOCacheService cache)
        {
            this.webHandler = webHandler;
            this.repo = repo;
            this.device = device;
            this.logging = logging;
            this.cache = cache;
        }

        public async Task<ResultState> UpdateAsync(CancellationToken cancellationToken)
        {
            if (!this.device.HasNetworkConnectivity)
            {
                return ResultState.ErrorNetwork;
            }

            try
            {
                var marker = this.repo.Mark();

                var overviewMap = await this.webHandler.GetHashesAsync(cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    return ResultState.Canceled;
                }

                AttachId(overviewMap);
                await this.UpdateImageAsync(overviewMap);
                this.repo.PersistEntry(overviewMap);

                foreach (var b in overviewMap.Buildings)
                {
                    AttachId(b, overviewMap.Id);
                    this.UpdateEntry(b);
                    foreach (var f in b.Floors)
                    {
                        AttachId(f, b.Id);
                        await this.UpdateImageAsync(f);
                        this.UpdateEntry(f);
                    }
                }

                this.repo.CleanUp(marker);

                return cancellationToken.IsCancellationRequested ? ResultState.Canceled : ResultState.Success;
            }
            catch (Exception ex)
            {
                this.logging.Exception(this, ex);
                return ResultState.Error;
            }
        }

        private static void AttachId(MapHashable entry, Guid? parentId = null)
        {
            if (parentId.HasValue)
            {
                entry.ParentId = parentId;
            }
        }

        private void UpdateEntry(MapHashable entry)
        {
            this.repo.PersistEntry(entry);
        }

        private async Task UpdateImageAsync(MapHashable entry)
        {
            string filePath = $"Map/{entry.Id}.png";
            var fileHash = Convert.ToBase64String(this.cache.GetSha1HashFromFile(filePath) ?? new byte[0]);

            if (fileHash != entry.Hash)
            {
                var imageStream = await this.webHandler.GetImageStreamAsync(entry.Id);
                await this.cache.WriteFileAsync(filePath, imageStream);
            }
        }
    }
}
