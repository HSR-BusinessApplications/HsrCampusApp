// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class WhNews
    {
        /// <summary>
        /// Gets or sets the ID of the post
        /// </summary>
        [DataMember]
        public string NewsId { get; set; }

        /// <summary>
        /// Gets or sets the text of a post
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the title of a post
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL to the original post
        /// </summary>
        [DataMember]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the date and time of a post
        /// This value will be delivered by the WebService with a TimeOffset information
        /// </summary>
        [DataMember]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the image as JPEG Base65 encoded
        /// </summary>
        [DataMember]
        public string PictureBitmap { get; set; }
    }
}
