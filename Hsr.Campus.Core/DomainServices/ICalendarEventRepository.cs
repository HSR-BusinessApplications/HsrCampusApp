// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.DomainServices
{
    using System;
    using System.Collections.Generic;
    using Model;

    public interface ICalendarEventRepository
    {
        bool HasEventsAfter(DateTime afterDate);

        IEnumerable<SqlCalendarEvent> RetrieveEventsAfter(DateTime afterDate, int amount);

        IEnumerable<SqlCalendarEvent> UpdateEventRange(IEnumerable<WhCalendarEvent> events);

        void DeleteEventEntries(DateTime beforeDate);

        void DeleteEventEntries();

        void Truncate();
    }
}
