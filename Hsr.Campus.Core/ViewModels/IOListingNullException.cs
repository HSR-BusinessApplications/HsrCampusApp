// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;

    [Serializable]
    public class IOListingNullException : Exception
    {
        public IOListingNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public IOListingNullException(string error)
            : base(error)
        {
            this.Error = error;
        }

        public IOListingNullException()
        {
        }

        protected IOListingNullException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        public string Error { get; set; }
    }
}
