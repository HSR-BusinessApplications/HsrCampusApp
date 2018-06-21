// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    [DataContract]
    public class AccountBalance
    {
        [DataMember(Name = "badgeSaldo")]
        [JsonProperty(PropertyName = "badgeSaldo")]
        public double? Deposit { get; set; }

        [DataMember]
        public DateTime LastUpdate { get; set; }
    }
}
