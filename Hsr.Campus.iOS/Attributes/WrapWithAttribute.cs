// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;

    public enum WrapWithController
    {
        None = 0,
        NavigationController = 1
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class WrapWithAttribute
        : Attribute
    {
        public WrapWithAttribute(WrapWithController with)
        {
            this.With = with;
        }

        public WrapWithController With { get; set; }
    }
}
