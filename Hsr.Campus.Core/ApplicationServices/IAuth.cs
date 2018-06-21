// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    public interface IAuth
    {
        string CalendarEvent { get; }

        string News { get; }

        string Map { get; }

        string Menu { get; }

        string Person { get; }
    }
}
