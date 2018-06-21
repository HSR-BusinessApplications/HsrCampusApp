// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.PlatformServices
{
    using System;
    using Foundation;
    using Hsr.Campus.Core.ApplicationServices;
    using Hsr.Campus.Core.Resources;
    using Security;

    public class AccountService
        : IAccountService
    {
        private const string Service = "HsrMobileServices";

        public bool HasAccount
        {
            get
            {
                SecStatusCode res;

                this.QueryAsRecord(out res);

                return res == SecStatusCode.Success;
            }
        }

        private static SecRecord Query => new SecRecord(SecKind.GenericPassword)
        {
            Service = Service
        };

        public void Save(Core.Model.Account account)
        {
            SecStatusCode res;

            var match = this.QueryAsRecord(out res);

            if (res == SecStatusCode.Success)
            {
                // Update
                match.Generic = account.SerializeToNSData();
                match.Account = account.UserName;
                match.ValueData = NSData.FromString(account.Token);
                SecKeyChain.Update(Query, match);
            }
            else
            {
                // create
                res = SecKeyChain.Add(new SecRecord(SecKind.GenericPassword)
                {
                    Label = AppResources.ApplicationTitle,
                    Account = account.UserName,
                    Service = Service,
                    ValueData = NSData.FromString(account.Token),
                    Generic = account.SerializeToNSData()
                });

                if (res != SecStatusCode.Success)
                {
                    throw new Exception("Unable to save account data");
                }
            }
        }

        public void Delete()
        {
            if (!this.HasAccount)
            {
                return;
            }

            SecStatusCode res;

            this.QueryAsRecord(out res);

            if (res != SecStatusCode.Success)
            {
                return;
            }

            res = SecKeyChain.Remove(Query);

            if (res != SecStatusCode.Success)
            {
                throw new Exception("Unable to delete account");
            }
        }

        public Core.Model.Account Retrieve()
        {
            SecStatusCode res;

            var match = this.QueryAsRecord(out res);

            if (res != SecStatusCode.Success)
            {
                return null;
            }

            try
            {
                return match.Generic.Deserialize<Core.Model.Account>();
            }
            catch
            {
                return null;
            }
        }

        private SecRecord QueryAsRecord(out SecStatusCode status) => SecKeyChain.QueryAsRecord(Query, out status);
    }
}
