// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.Utils
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class CellDefinitionAttributeException : Exception
    {
        public CellDefinitionAttributeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CellDefinitionAttributeException(string message)
            : base(message)
        {
        }

        public CellDefinitionAttributeException()
        {
        }

        protected CellDefinitionAttributeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
