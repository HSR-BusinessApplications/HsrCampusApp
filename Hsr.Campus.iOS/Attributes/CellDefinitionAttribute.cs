// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CellDefinitionAttribute
        : Attribute
    {
        public CellDefinitionAttribute(string identifier, float height)
        {
            this.Identifier = identifier;
            this.Height = height;
        }

        public string Identifier { get; set; }

        public float Height { get; set; }
    }
}
