// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class StoryboardAttribute
        : Attribute
    {
        public StoryboardAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
