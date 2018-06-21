// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.DomainServices
{
    using Model;

    public interface IAccountBalanceRepository
    {
        AccountBalance RetrieveCurrentState();

        void PersistBalance(AccountBalance balance);

        void Remove();
    }
}
