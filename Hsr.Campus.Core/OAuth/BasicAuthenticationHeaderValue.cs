// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// This code is based on Thinktecture.IdentityModel:
// Copyright (c) 2013 Dominick Baier, Brock Allen. All rights reserverd.
// See {project root}/licenses/Thinktecture.IdentiyModel License.txt
namespace Hsr.Campus.Core.OAuth
{
    using System;
    using System.Net.Http.Headers;
    using System.Text;

    public class BasicAuthenticationHeaderValue : AuthenticationHeaderValue
    {
        public BasicAuthenticationHeaderValue(string userName, string password)
            : base("Basic", EncodeCredential(userName, password))
        {
        }

        private static string EncodeCredential(string userName, string password)
        {
            var encoding = Encoding.UTF8;
            string credential = $"{userName}:{password}";

            return Convert.ToBase64String(encoding.GetBytes(credential));
        }
    }
}
