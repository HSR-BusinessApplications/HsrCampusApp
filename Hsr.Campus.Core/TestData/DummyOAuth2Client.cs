// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// Classes with test data may only be compiled in the Test-Build
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Hsr.Campus.Core.OAuth;

    public class DummyOAuth2Client
    {
        private HttpClient client;
        private ClientAuthenticationStyle authenticationStyle;
        private Uri address;
        private string clientId;
        private string clientSecret;

        public DummyOAuth2Client(Uri address)
            : this(address, null)
        {
        }

        public DummyOAuth2Client(Uri address, HttpClient httpClient)
        {
            this.client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.client.BaseAddress = address;

            this.address = address;
            this.authenticationStyle = ClientAuthenticationStyle.None;
        }

        public DummyOAuth2Client(Uri address, string clientId, string clientSecret, HttpClient httpClient, ClientAuthenticationStyle style = ClientAuthenticationStyle.BasicAuthentication)
            : this(address, httpClient)
        {
            switch (style)
            {
                case ClientAuthenticationStyle.BasicAuthentication:
                    this.client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(clientId, clientSecret);
                    break;
                case ClientAuthenticationStyle.PostValues:
                    this.authenticationStyle = style;
                    this.clientId = clientId;
                    this.clientSecret = clientSecret;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }

        public enum ClientAuthenticationStyle
        {
            BasicAuthentication,
            PostValues,
            None
        }

        public string CreateCodeFlowUrl(string clientId, string scope = null, string redirectUri = null, string state = null, Dictionary<string, string> additionalValues = null)
        {
            return this.CreateAuthorizeUrl(clientId, ResponseTypes.Code, scope, redirectUri, state, additionalValues);
        }

        public Task<TokenResponse> RequestAuthorizationCodeAsync(string code, string redirectUri, Dictionary<string, string> additionalValues = null)
        {
            var fields = new Dictionary<string, string>
            {
                [OAuth2Constants.GrantType] = GrantTypes.AuthorizationCode,
                [OAuth2Constants.Code] = code,
                [OAuth2Constants.RedirectUri] = redirectUri
            };

            return Task.Run(() => this.Request(this.Merge(fields, additionalValues)));
        }

        public Task<TokenResponse> RequestRefreshTokenAsync(string refreshToken, Dictionary<string, string> additionalValues = null)
        {
            var fields = new Dictionary<string, string>
            {
                [OAuth2Constants.GrantType] = GrantTypes.RefreshToken,
                [OAuth2Constants.RefreshToken] = refreshToken
            };

            return Task.Run(() => this.Request(this.Merge(fields, additionalValues)));
        }

        private static string CreateAuthorizeUrl(Uri endpoint, Dictionary<string, string> values)
        {
            var qs = string.Join("&", values.Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}").ToArray());
            return $"{endpoint.AbsoluteUri}?{qs}";
        }

        private string CreateAuthorizeUrl(string clientId, string responseType, string scope = null, string redirectUri = null, string state = null, Dictionary<string, string> additionalValues = null)
        {
            var values = new Dictionary<string, string>
            {
                [OAuth2Constants.ClientId] = clientId,
                [OAuth2Constants.ResponseType] = responseType
            };

            if (!string.IsNullOrWhiteSpace(scope))
            {
                values.Add(OAuth2Constants.Scope, scope);
            }

            if (!string.IsNullOrWhiteSpace(redirectUri))
            {
                values.Add(OAuth2Constants.RedirectUri, redirectUri);
            }

            if (!string.IsNullOrWhiteSpace(state))
            {
                values.Add(OAuth2Constants.State, state);
            }

            return CreateAuthorizeUrl(this.address, this.Merge(values, additionalValues));
        }

        private TokenResponse Request(Dictionary<string, string> form)
        {
            var content = "{\"access_token\":\"dummyToken\",\"token_type\":\"Bearer\",\"expires_in\":300,\"refresh_token\":\"dummyRefreshToken\",\"identifier\":\"dummy\"}";
            return new TokenResponse(content);
        }

        private Dictionary<string, string> Merge(Dictionary<string, string> explicitValues, Dictionary<string, string> additionalValues = null)
        {
            var merged = explicitValues;

            if (this.authenticationStyle == ClientAuthenticationStyle.PostValues)
            {
                merged.Add(OAuth2Constants.ClientId, this.clientId);
                merged.Add(OAuth2Constants.ClientSecret, this.clientSecret);
            }

            if (additionalValues != null)
            {
                merged = explicitValues.Concat(additionalValues.Where(add => !explicitValues.ContainsKey(add.Key))).ToDictionary(final => final.Key, final => final.Value);
            }

            return merged;
        }
    }
}
#endif
