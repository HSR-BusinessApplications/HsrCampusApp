// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Account
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public DateTime TokenRetrieved { get; set; }

        [DataMember]
        public DateTime TokenValidUntil { get; set; }

        [DataMember]
        public string RefreshToken { get; set; }
    }
}
