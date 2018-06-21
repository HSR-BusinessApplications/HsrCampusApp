// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [DataContract(Name = "io")]
    public sealed class IOListing
    {
        /// <summary>
        /// Gets or sets a value indicating whether the IOListing is a directory.
        /// </summary>
        [DataMember]
        public bool IsDirectory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current item is a Link (lnk).
        /// </summary>
        [DataMember]
        public bool IsLink { get; set; }

        /// <summary>
        /// Gets or sets the directory or file name (without path).
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path to the directory or file. Directory path end with "/".
        /// </summary>
        [DataMember]
        public string FullPath { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the file was last modified.
        /// </summary>
        [DataMember]
        [JsonProperty(ItemConverterType = typeof(JavaScriptDateTimeConverter))]
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets the size (in human-readable form) of a file.
        /// </summary>
        [DataMember]
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets a value the size (in bytes) of a file.
        /// </summary>
        [DataMember]
        public long RawSize { get; set; }

        /// <summary>
        /// Gets or sets the URL on the "Skripte" server.
        /// </summary>
        [DataMember]
        public string Url { get; set; }

        [IgnoreDataMember]
        public bool IsLocal { get; set; }
    }
}
