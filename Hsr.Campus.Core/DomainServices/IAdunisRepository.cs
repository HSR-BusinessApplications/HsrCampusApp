// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.DomainServices
{
    using System;
    using System.Collections.Generic;
    using Model;

    public interface IAdunisRepository
    {
        DateTime LastUpdated { get; }

        // Timeperiods
        void PersistTimeperiods(IEnumerable<SqlTimeperiod> periods);

        void UpdateTimeperiod(SqlTimeperiod period);

        IEnumerable<SqlTimeperiod> RetrieveAllTimeperiods();

        IEnumerable<SqlTimeperiod> RetrieveNonEmptyTimeperiods();

        SqlTimeperiod RetrieveSingleTimeperiod(DateTime now);

        SqlTimeperiod RetrieveTimeperiodById(int termId);

        // Appointments
        void PersistAppointments(IEnumerable<SqlAdunisAppointment> appointments);

        IEnumerable<SqlAdunisAppointment> RetrieveAppointments(SqlTimeperiod period);

        IEnumerable<SqlAdunisAppointment> RetrieveAppointmentsForRange(SqlTimeperiod period, DateTime dateStart, DateTime dateEnd);

        IEnumerable<SqlAdunisAppointment> RetrieveAppointmentsForDay(SqlTimeperiod period, DateTime date);

        void DeleteAppointments(SqlTimeperiod period);

        // Clear All
        void Truncate();
    }
}
