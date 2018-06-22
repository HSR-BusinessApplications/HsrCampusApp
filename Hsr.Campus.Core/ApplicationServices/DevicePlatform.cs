// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    public enum DevicePlatform
    {
        Neutral = 0,
        Android,

#pragma warning disable SA1300 // Element must begin with upper-case letter

        iOS
#pragma warning restore SA1300 // Element must begin with upper-case letter
    }
}
