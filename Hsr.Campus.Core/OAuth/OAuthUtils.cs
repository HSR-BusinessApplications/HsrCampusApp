// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

﻿namespace Hsr.Campus.Core.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security;
    using System.Threading;
    using System.Threading.Tasks;
    using Hsr.Campus.Core.ApplicationServices;
    using Hsr.Campus.Core.OAuth;
    using Hsr.Campus.Core.ViewModels;
    using Infrastructure;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Platform;
    using MvvmCross.Plugins.WebBrowser;
    using Plugin.Settings.Abstractions;
    using Resources;

    public class OAuthUtils : IOAuthUtils
    {
        public const string OAuthSchema = "hsrcampus://";

        private static readonly string StateKey = typeof(OAuthUtils) + "State";

        private static readonly Semaphore BearingSemaphone = new Semaphore(1, 1);

        private readonly string oAuthClient;
        private readonly string oAuthSecret;

        private readonly ISettings settings;
        private readonly IAccountService account;
        private readonly IHttpClientConfiguration httpClientConfiguration;
        private readonly IUserInteractionService userInteraction;
        private readonly IDevice device;
        private readonly IMvxWebBrowserTask browser;
        private readonly IServiceApi serviceApi;

        public OAuthUtils(ISettings settings, IDevice device, IUserInteractionService userInteraction, IAccountService account, IHttpClientConfiguration httpClientConfiguration, IMvxWebBrowserTask browser, IServiceApi serviceApi)
        {
            this.settings = settings;
            this.device = device;
            this.userInteraction = userInteraction;
            this.account = account;
            this.httpClientConfiguration = httpClientConfiguration;
            this.State = this.settings.GetValueOrDefault(StateKey, Guid.NewGuid().ToString());
            this.browser = browser;
            this.serviceApi = serviceApi;
            this.oAuthClient = serviceApi.OAuthClient;
            this.oAuthSecret = serviceApi.OAuthSecret;
        }

        public string State { get; private set; }

        private string ClientId
        {
            get
            {
                switch (this.device.Platform)
                {
                    case DevicePlatform.Android:
                        return this.oAuthClient + "Android";
                    case DevicePlatform.WindowsPhone:
                        return this.oAuthClient + "WindowsPhone";
                    case DevicePlatform.iOS:
                        return this.oAuthClient + "iOS";
                    default:
                        Debug.Assert(false, "Unbekannte Plattform");
                        return this.oAuthClient + "WindowsPhone";
                }
            }
        }

        public string CreateCodeFlowUrl()
        {
            this.SaveState();

            return this.Client(this.serviceApi.AuthUri).CreateCodeFlowUrl(this.ClientId, "read", OAuthSchema, this.State);
        }

        public void DisplayCodeFlowUrl(IMvxViewModel viewModel)
        {
            var codeFlowUrl = this.CreateCodeFlowUrl();
            this.browser.ShowWebPage(codeFlowUrl);
        }

        public void DisplalyWebGuiUri()
        {
            this.browser.ShowWebPage(this.serviceApi.WebGuiUri);
        }

        public Task<TokenResponse> RefreshTokenAsync(string refreshToken) => this.Client(this.serviceApi.TokenUri, this.ClientId, this.oAuthSecret).RequestRefreshTokenAsync(refreshToken);

        // ReSharper disable once MemberCanBePrivate.Global
        public Task<TokenResponse> AuthTokenAsync(AuthorizeResponse response) => this.Client(this.serviceApi.TokenUri, this.ClientId, this.oAuthSecret).RequestAuthorizationCodeAsync(response.Code, OAuthSchema);

        /// <summary>
        /// Parses the authorization token
        /// Checks and resets the state
        /// </summary>
        /// <param name="rawString">Should be something like: hsrcampus://?code=a43f9a4d26704a82b6e913f1eb664515&amp;state=628c5b48-693b-4518-9177-746548403a6a</param>
        /// <returns>Parsed answer</returns>
        public AuthorizeResponse AuthToken(string rawString)
        {
            var t = new AuthorizeResponse(rawString);
            return this.ValidateAndResetState(t);
        }

        public AuthorizeResponse AuthToken(IDictionary<string, string> rawMap)
        {
            var t = new AuthorizeResponse(rawMap);
            return this.ValidateAndResetState(t);
        }

        public async Task OAuthSetupAsync(AuthorizeResponse authToken)
        {
            this.ResetState();

            if (!string.IsNullOrEmpty(authToken.Error))
            {
                this.userInteraction.Dialog(AppResources.TileSetupAccount, string.Format(AppResources.OAuthError, authToken.Error));
                return;
            }

            var token = await this.AuthTokenAsync(authToken);

            if (token.IsError)
            {
                this.userInteraction.Dialog(AppResources.TileSetupAccount, string.Format(AppResources.OAuthError, authToken.Error));
                return;
            }

            this.SaveToken(token);
        }

        public void SaveToken(TokenResponse token)
        {
            this.account.Save(new Model.Account()
            {
                UserName = token.Identifier, Token = token.AccessToken, RefreshToken = token.RefreshToken, TokenRetrieved = DateTime.UtcNow, TokenValidUntil = DateTime.UtcNow.AddSeconds(token.ExpiresIn)
            });
        }

        public Task<string> BearerTokenAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    Debug.WriteLine("WaitOne " + Environment.CurrentManagedThreadId);
                    BearingSemaphone.WaitOne();
                    Debug.WriteLine("Obtain " + Environment.CurrentManagedThreadId);

                    if (!this.account.HasAccount)
                    {
                        throw new SecurityException("Account does not exist");
                    }

                    var activeAccount = this.account.Retrieve();

                    if (DateTime.UtcNow.AddSeconds(60) < activeAccount.TokenValidUntil)
                    {
                        return activeAccount.Token;
                    }

                    var token = this.RefreshTokenAsync(activeAccount.RefreshToken).Result;

                    if (token.IsError || string.IsNullOrEmpty(token.AccessToken) || string.IsNullOrEmpty(token.RefreshToken))
                    {
                        throw new OAuthExpiredException(token.Error);
                    }

                    this.SaveToken(token);

                    return token.AccessToken;
                }
                finally
                {
                    Debug.WriteLine("Release " + Environment.CurrentManagedThreadId);
                    BearingSemaphone.Release();
                }
            });
        }

        private OAuth2Client Client(string url, string client = null, string secret = null) => new OAuth2Client(new Uri(url), client, secret, new NativeHttpClient(this.httpClientConfiguration));

        private AuthorizeResponse ValidateAndResetState(AuthorizeResponse t)
        {
            if (t.State != this.State)
            {
                throw new SecurityException("Control-State modified.");
            }

            this.ResetState();
            this.SaveState();

            return t;
        }

        private void ResetState()
        {
            this.State = Guid.NewGuid().ToString();
        }

        private void SaveState()
        {
            this.settings.AddOrUpdateValue(StateKey, this.State);
        }
    }
}
