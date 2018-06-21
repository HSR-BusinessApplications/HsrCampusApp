// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using Model;

    public interface IAdunisSync
    {
        Task<ResultState> UpdateAsync(bool force, CancellationToken cancellationToken);

        Task<ResultState> UpdateAppointmentsAsync(bool force, SqlTimeperiod period, CancellationToken cancellationToken);
    }
}
