// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class IOListingRoot
    {
        [DataMember(Name = "data")]
        public IOListing[] Data { get; set; }

        [DataMember(Name = "exception")]
        public string Exception { get; set; }
    }
}
