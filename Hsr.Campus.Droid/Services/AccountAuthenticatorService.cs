// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Services
{
    using Android.App;
    using Android.Content;
    using Android.OS;

    [Service(Exported = true)]
    [IntentFilter(new string[] { "android.accounts.AccountAuthenticator" })]
    [MetaData("android.accounts.AccountAuthenticator", Resource = "@xml/authenticator")]
    public class AccountAuthenticatorService : Service
    {
        private AccountAuthenticator authenticator;

        public override void OnCreate()
        {
            base.OnCreate();
            this.authenticator = new AccountAuthenticator(this);
        }

        public override IBinder OnBind(Intent intent) => this.GetAuthenticator().IBinder;

        private AccountAuthenticator GetAuthenticator() => this.authenticator;
    }

    /* Enclosing the Account Authenticator in here as well */
}
