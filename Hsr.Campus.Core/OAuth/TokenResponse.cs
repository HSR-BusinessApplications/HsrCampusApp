// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// This code is based on Thinktecture.IdentityModel:
// Copyright (c) 2013 Dominick Baier, Brock Allen. All rights reserverd.
// See {project root}/licenses/Thinktecture.IdentiyModel License.txt
namespace Hsr.Campus.Core.OAuth
{
    using System.Net;
    using Newtonsoft.Json.Linq;

    public class TokenResponse
    {
        private readonly JObject json;
        private readonly bool isHttpError;

        // Error- ctor
        public TokenResponse()
        {
            this.isHttpError = true;
        }

        public TokenResponse(string raw)
        {
            this.json = JObject.Parse(raw);
        }

        public string AccessToken => this.GetStringOrNull(OAuth2Constants.AccessToken);

        public string Identifier => this.GetStringOrNull(OAuth2Constants.Identifier);

        public string Error => this.GetStringOrNull(OAuth2Constants.Error);

        public bool IsError
        {
            get
            {
                return this.isHttpError ||
                        !string.IsNullOrWhiteSpace(this.GetStringOrNull(OAuth2Constants.Error));
            }
        }

        public long ExpiresIn => this.GetLongOrNull(OAuth2Constants.ExpiresIn);

        public string RefreshToken => this.GetStringOrNull(OAuth2Constants.RefreshToken);

        protected virtual string GetStringOrNull(string name)
        {
            JToken value;
            if (this.json != null && this.json.TryGetValue(name, out value))
            {
                return value.ToString();
            }

            return null;
        }

        protected virtual long GetLongOrNull(string name)
        {
            JToken value;
            if (this.json != null && this.json.TryGetValue(name, out value))
            {
                long longValue = 0;
                if (long.TryParse(value.ToString(), out longValue))
                {
                    return longValue;
                }
            }

            return 0;
        }
    }
}
