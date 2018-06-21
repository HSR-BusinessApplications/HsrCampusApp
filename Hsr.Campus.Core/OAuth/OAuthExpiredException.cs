// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.OAuth
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class OAuthExpiredException : Exception
    {
        public OAuthExpiredException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OAuthExpiredException(string error)
        {
            this.Error = error;
        }

        public OAuthExpiredException()
        {
        }

        protected OAuthExpiredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string Error { get; set; }
    }
}
