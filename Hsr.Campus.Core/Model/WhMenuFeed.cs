// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class WhMenuFeed
    {
        /// <summary>
        /// Gets or sets the ID of the menu feed
        /// The value is used for clear identification and the web request
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the short name of the feed which is displayed if theres little space
        /// </summary>
        [DataMember]
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the name of the menu feed
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a list of all days which have a menu
        /// </summary>
        [DataMember]
        public IEnumerable<WhMenuDay> Days { get; set; }
    }
}
