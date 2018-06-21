// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.PlatformServices
{
    using System;
    using Android.Accounts;
    using Android.OS;
    using Core.ApplicationServices;
    using MvvmCross.Platform;
    using MvvmCross.Platform.Droid.Platform;

    public class AccountService : IAccountService
    {
        public const string AccountType = "ch.hsr.apps";
        public const string KeyRefreshToken = "refreshToken";
        public const string KeyAuthValidUntil = "authTokenValidUntil";
        public const string KeyAuthRetrieved = "authTokenRetrieved";

        public bool HasAccount => this.AM.GetAccountsByType(AccountType).Length > 0;

        private AccountManager AM => AccountManager.Get(Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);

        public void Save(Core.Model.Account account)
        {
            if (!this.HasAccount)
            {
                var userData = new Bundle();

                userData.PutString(AccountManager.KeyAccountName, account.UserName);
                userData.PutString(AccountManager.KeyAccountType, AccountType);
                userData.PutString(AccountManager.KeyAuthtoken, account.Token);
                userData.PutString(KeyRefreshToken, account.RefreshToken);
                userData.PutString(KeyAuthValidUntil, account.TokenValidUntil.Ticks.ToString());
                userData.PutString(KeyAuthRetrieved, account.TokenRetrieved.Ticks.ToString());

                this.AM.AddAccount(AccountType, "client", null, userData, null, null, null);
            }
            else
            {
                var acc = this.AM.GetAccountsByType(AccountType)[0];
                this.AM.SetUserData(acc, AccountManager.KeyAuthtoken, account.Token);
                this.AM.SetUserData(acc, KeyRefreshToken, account.RefreshToken);
                this.AM.SetUserData(acc, KeyAuthValidUntil, account.TokenValidUntil.Ticks.ToString());
                this.AM.SetUserData(acc, KeyAuthRetrieved, account.TokenRetrieved.Ticks.ToString());
            }
        }

        public void Delete()
        {
            foreach (var androidAccount in this.AM.GetAccountsByType(AccountType))
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.LollipopMr1)
                {
                    var context = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
                    this.AM.RemoveAccount(androidAccount, context.Activity, null, null);
                }
                else
                {
#pragma warning disable 618
                    this.AM.RemoveAccount(androidAccount, null, null);
#pragma warning restore 618
                }
            }
        }

        public Core.Model.Account Retrieve()
        {
            var accounts = this.AM.GetAccountsByType(AccountType);

            if (accounts.Length == 0)
            {
                return null;
            }

            var account = accounts[0];

            return new Core.Model.Account()
            {
                UserName = this.AM.GetUserData(account, AccountManager.KeyAccountName),
                RefreshToken = this.AM.GetUserData(account, KeyRefreshToken),
                Token = this.AM.GetUserData(account, AccountManager.KeyAuthtoken),
                TokenRetrieved = new DateTime(long.Parse(this.AM.GetUserData(account, KeyAuthRetrieved)), DateTimeKind.Utc),
                TokenValidUntil = new DateTime(long.Parse(this.AM.GetUserData(account, KeyAuthValidUntil)), DateTimeKind.Utc)
            };
        }
    }
}
