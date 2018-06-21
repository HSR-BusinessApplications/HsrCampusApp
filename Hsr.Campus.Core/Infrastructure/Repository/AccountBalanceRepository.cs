// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.Repository
{
    using System;
    using System.Globalization;
    using DomainServices;
    using Model;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Plugin.Settings.Abstractions;

    public class AccountBalanceRepository : IAccountBalanceRepository
    {
        private static readonly string StateKey = typeof(AccountBalance).FullName + "State";

        private readonly ISettings settings;

        public AccountBalanceRepository(ISettings settings)
        {
            this.settings = settings;
        }

        public AccountBalance RetrieveCurrentState()
        {
            var state = this.settings.GetValueOrDefault(StateKey, default(string));

            return string.IsNullOrEmpty(state) ? new AccountBalance { Deposit = 0, LastUpdate = DateTime.MinValue } : state.CreateFromJsonString<AccountBalance>();
        }

        public void PersistBalance(AccountBalance balance)
        {
            this.settings.AddOrUpdateValue(StateKey, JsonConvert.SerializeObject(balance, new IsoDateTimeConverter { Culture = CultureInfo.CurrentUICulture }));
        }

        public void Remove()
        {
            this.settings.AddOrUpdateValue(StateKey, default(string));
        }
    }
}
