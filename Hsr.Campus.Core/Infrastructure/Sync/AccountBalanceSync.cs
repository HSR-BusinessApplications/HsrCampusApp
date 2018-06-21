// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.Sync
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Hsr.Campus.Core.OAuth;

    public class AccountBalanceSync : IAccountBalanceSync
    {
        private readonly ILogging logging;
        private readonly IDevice device;
        private readonly IAccountBalanceWebHandler handler;
        private readonly IAccountBalanceRepository repo;

        public AccountBalanceSync(IAccountBalanceWebHandler handler, IAccountBalanceRepository repo, ILogging logging, IDevice device)
        {
            this.handler = handler;
            this.repo = repo;
            this.logging = logging;
            this.device = device;
        }

        public async Task<ResultState> UpdateAsync(CancellationToken cancellationToken)
        {
            if (!this.device.HasNetworkConnectivity)
            {
                return ResultState.Error;
            }

            try
            {
                var stateCurrent = await this.handler.GetCurrentStateAsync(cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    return ResultState.Canceled;
                }

                stateCurrent.LastUpdate = DateTime.Now;

                this.repo.PersistBalance(stateCurrent);

                return ResultState.Success;
            }
            catch (OAuthExpiredException)
            {
                return ResultState.OAuthExpired;
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
