// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// This code is based on Thinktecture.IdentityModel:
// Copyright (c) 2013 Dominick Baier, Brock Allen. All rights reserverd.
// See {project root}/licenses/Thinktecture.IdentiyModel License.txt
namespace Hsr.Campus.Core.OAuth
{
    using System;
    using System.Collections.Generic;

    public sealed class AuthorizeResponse
    {
        private readonly string raw;
        private readonly Dictionary<string, string> values;

        public AuthorizeResponse(string raw)
        {
            this.raw = raw;
            this.values = new Dictionary<string, string>();
            this.ParseRawString();
        }

        public AuthorizeResponse(IDictionary<string, string> map)
        {
            this.values = new Dictionary<string, string>(map);
        }

        public string Code => this.TryGet(OAuth2Constants.Code);

        public string Error => this.TryGet(OAuth2Constants.Error);

        public string State => this.TryGet(OAuth2Constants.State);

        private void ParseRawString()
        {
            string[] fragments = null;

            if (this.raw.Contains("#"))
            {
                fragments = this.raw.Split('#');
            }
            else if (this.raw.Contains("?"))
            {
                fragments = this.raw.Split('?');
            }
            else
            {
                throw new InvalidOperationException("Malformed callback URL");
            }

            var qparams = fragments[1].Split('&');

            foreach (var param in qparams)
            {
                var parts = param.Split('=');

                if (parts.Length == 2)
                {
                    this.values.Add(parts[0], parts[1]);
                }
                else
                {
                    throw new InvalidOperationException("Malformed callback URL.");
                }
            }
        }

        private string TryGet(string type)
        {
            string value;
            return this.values.TryGetValue(type, out value) ? value : null;
        }
    }
}
