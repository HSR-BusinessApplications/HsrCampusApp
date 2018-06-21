// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Services
{
    using System;
    using Android.Accounts;
    using Android.Content;
    using Android.OS;
    using PlatformServices;

    public class AccountAuthenticator : AbstractAccountAuthenticator
    {
        private readonly Context context;

        public AccountAuthenticator(Context context)
            : base(context)
        {
            this.context = context;
        }

        // Adds an account of the specified accountType.
        public override Bundle AddAccount(AccountAuthenticatorResponse response, string accountType, string authTokenType, string[] requiredFeatures, Bundle options)
        {
            var acc = new Account(options.GetString(AccountManager.KeyAccountName), accountType);

            var am = AccountManager.Get(this.context);

            am.AddAccountExplicitly(acc, null, options);

            return null;
        }

        // Stub Method
        public override Bundle ConfirmCredentials(AccountAuthenticatorResponse response, Account account, Bundle options)
        {
            Console.WriteLine("Confirm Credentials");
            return null;
        }

        // Stub Method
        public override Bundle GetAuthToken(AccountAuthenticatorResponse response, Account account, string authTokenType, Bundle options)
        {
            Console.WriteLine("Get Auth Token");
            return null;
        }

        // Stub Method
        public override string GetAuthTokenLabel(string authTokenType)
        {
            var accountService = new AccountService();

            if (!accountService.HasAccount)
            {
                return null;
            }

            return accountService.Retrieve().Token;
        }

        // Stub Method
        public override Bundle UpdateCredentials(AccountAuthenticatorResponse response, Account account, string authTokenType, Bundle options)
        {
            Console.WriteLine("Update Credentials");
            return null;
        }

        // Stub Method
        public override Bundle HasFeatures(AccountAuthenticatorResponse response, Account account, string[] features)
        {
            Console.WriteLine("Has Feature");
            return null;
        }

        // Stub Method
        public override Bundle EditProperties(AccountAuthenticatorResponse response, string accountType)
        {
            Console.WriteLine("Edit Properties");
            return null;
        }
    }
}
