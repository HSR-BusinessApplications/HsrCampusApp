// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.DomainServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Model;

    public interface IMenuWebHandler
    {
        Task<IEnumerable<WhMenuFeed>> GetFeedsAsync(CancellationToken cancellationToken);

        Task<string> GetMenuHtmlAsync(string feedId, DateTime dayDate, CancellationToken cancellationToken);
    }
}
