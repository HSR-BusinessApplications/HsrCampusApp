// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using SQLite;

    [Table("News2")]
    public class SqlNews : INotifyPropertyChanged
    {
        private string title;
        private string message;
        private string url;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the ID of the post
        /// </summary>
        [Column("NewsId")]
        [PrimaryKey]
        public string NewsId { get; set; }

        /// <summary>
        /// Gets or sets the key of the feed to which the post belongs
        /// </summary>
        [Column("FeedKey")]
        public string FeedKey { get; set; }

        /// <summary>
        /// Gets or sets the titel of the post
        /// </summary>
        [Column("Title")]
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (value == this.title)
                {
                    return;
                }

                this.title = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the text of the post
        /// </summary>
        [Column("Message")]
        public string Message
        {
            get
            {
                return this.message;
            }

            set
            {
                if (value == this.message)
                {
                    return;
                }

                this.message = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the URL to the original post
        /// </summary>
        [Column("Url")]
        public string Url
        {
            get
            {
                return this.url;
            }

            set
            {
                if (value == this.url)
                {
                    return;
                }

                this.url = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the date and time of the post
        /// This value will be delivered by the WebService with a TimeOffset information
        /// </summary>
        [Column("Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the path to the image from the post
        /// Is <code>null</code> if no image exists
        /// </summary>
        [Column("PicturePath")]
        public string PicturePath { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the last update of the post (UTC)
        /// </summary>
        [Column("LastUpdated")]
        public DateTime LastUpdated { get; set; }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
