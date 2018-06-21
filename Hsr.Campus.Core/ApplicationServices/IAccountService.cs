// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    using Model;

    public interface IAccountService
    {
        bool HasAccount { get; }

        void Save(Account account);

        void Delete();

        Account Retrieve();
    }
}
