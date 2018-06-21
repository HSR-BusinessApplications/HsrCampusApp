// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Model;

    public interface INewsSync
    {
        Task<ResultState> UpdateFeedsAsync(bool force, CancellationToken cancellationToken);

        Task<Tuple<ResultState, IEnumerable<SqlNews>>> UpdateNewsAsync(bool force, SqlNewsFeed feed, DateTime before, CancellationToken cancellationToken);
    }
}
