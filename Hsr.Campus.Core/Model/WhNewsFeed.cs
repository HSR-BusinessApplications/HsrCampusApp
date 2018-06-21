// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class WhNewsFeed
    {
        /// <summary>
        /// Gets or sets the key of the feed
        /// The value is used for clear identification and the web request
        /// </summary>
        [DataMember]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the name of the feed
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the feed is displayed in the "HSR-Sport" tab
        /// </summary>
        [DataMember]
        public bool HsrSport { get; set; }
    }
}
