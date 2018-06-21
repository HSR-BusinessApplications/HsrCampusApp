// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core
{
    using System;

    public class StartUpHint
    {
        public Type ViewModel { get; set; }

        public object Parameter { get; set; }
    }
}
