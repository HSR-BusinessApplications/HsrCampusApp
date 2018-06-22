// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// Classes with test data may only be compiled in the Test-Build
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Hsr.Campus.Core.ApplicationServices;
    using Hsr.Campus.Core.Infrastructure;
    using Hsr.Campus.Core.OAuth;
    using Hsr.Campus.Core.Resources;
    using Hsr.Campus.Core.ViewModels;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Platform;
    using Plugin.Settings.Abstractions;

    public class DummyOAuthUtils : IOAuthUtils
    {
        public const string OAuthSchema = "hsrcampus://";

        private static readonly string StateKey = typeof(DummyOAuthUtils) + "State";

        private static readonly Semaphore BearingSemaphone = new Semaphore(1, 1);

        private readonly string oAuthClient;
        private readonly string oAuthSecret;

        private readonly ISettings settings;
        private readonly IAccountService account;
        private readonly IHttpClientConfiguration httpClientConfiguration;
        private readonly IUserInteractionService userInteraction;
        private readonly IDevice device;
        private readonly IMvxNavigationService navigationService;
        private readonly IServiceApi serviceApi;

        public DummyOAuthUtils(ISettings settings, IDevice device, IUserInteractionService userInteraction, IAccountService account, IHttpClientConfiguration httpClientConfiguration, IMvxNavigationService navigationService, IServiceApi serviceApi)
        {
            this.settings = settings;
            this.device = device;
            this.userInteraction = userInteraction;
            this.account = account;
            this.httpClientConfiguration = httpClientConfiguration;
            this.State = this.settings.GetValueOrDefault(StateKey, Guid.NewGuid().ToString());
            this.navigationService = navigationService;
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
                    case DevicePlatform.iOS:
                        return this.oAuthClient + "iOS";
                    default:
                        Debug.Assert(false, "Unbekannte Plattform");
                        return this.oAuthClient + "WindowsPhone";
                }
            }
        }

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

        public Task<TokenResponse> AuthTokenAsync(AuthorizeResponse response) => this.Client(this.serviceApi.TokenUri, this.ClientId, this.oAuthSecret).RequestAuthorizationCodeAsync(response.Code, OAuthSchema);

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

                    var account = this.account.Retrieve();

                    if (DateTime.UtcNow.AddSeconds(60) < account.TokenValidUntil)
                    {
                        return account.Token;
                    }

                    var token = this.RefreshTokenAsync(account.RefreshToken).Result;

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

        public string CreateCodeFlowUrl()
        {
            this.SaveState();

            return this.Client(this.serviceApi.AuthUri).CreateCodeFlowUrl(this.ClientId, "read", OAuthSchema, this.State);
        }

        public void DisplayCodeFlowUrl(IMvxViewModel viewModel)
        {
            var codeFlowUrl = this.CreateCodeFlowUrl();
            this.userInteraction.Dialog(AppResources.TileSetupAccount, "Accept the token request?", () => this.OAuthAcceptedAction(codeFlowUrl, viewModel), () => this.OAuthCancelledAction(viewModel));
        }

        public void DisplalyWebGuiUri()
        {
            this.userInteraction.Toast("Only dummy token available, URL will not be shown!", ToastTime.Medium);
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

        public Task<TokenResponse> RefreshTokenAsync(string refreshToken) => this.Client(this.serviceApi.TokenUri, this.ClientId, this.oAuthSecret).RequestRefreshTokenAsync(refreshToken);

        public void SaveToken(TokenResponse token)
        {
            this.account.Save(new Model.Account()
            {
                UserName = token.Identifier,
                Token = token.AccessToken,
                RefreshToken = token.RefreshToken,
                TokenRetrieved = DateTime.UtcNow,
                TokenValidUntil = DateTime.UtcNow.AddSeconds(token.ExpiresIn)
            });
        }

        private DummyOAuth2Client Client(string url, string client = null, string secret = null) => new DummyOAuth2Client(new Uri(url), client, secret, new NativeHttpClient(this.httpClientConfiguration));

        private async void OAuthAcceptedAction(string codeFlowUrl, IMvxViewModel viewModel)
        {
            var accountViewModel = viewModel as AccountViewModel;
            var authorizeResponse = this.AuthToken(codeFlowUrl);
            await this.OAuthSetupAsync(authorizeResponse);
            await this.navigationService.Close(accountViewModel);
            await this.navigationService.Navigate<AccountViewModel>();
        }

        private async void OAuthCancelledAction(IMvxViewModel viewModel)
        {
            var accountViewModel = viewModel as AccountViewModel;
            this.account.Delete();
            await this.navigationService.Close(accountViewModel);
            this.userInteraction.Toast(AppResources.OAuthErrorDenied, ToastTime.Medium);
        }

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
#endif
