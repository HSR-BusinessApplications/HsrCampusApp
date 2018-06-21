// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.DomainServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using Model;

    public interface IAccountBalanceWebHandler
    {
        Task<AccountBalance> GetCurrentStateAsync(CancellationToken cancellationToken);
    }
}
