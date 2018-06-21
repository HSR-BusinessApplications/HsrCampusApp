// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using MvvmCross.Core.ViewModels;
    using Resources;

    public class WeekViewModel : MvxViewModel
    {
        public int CalendarWeek
        {
            get; set;
        }

        public int PeriodWeek
        {
            get; set;
        }

        public new DateTime Start { get; set; }

        public override string ToString() => string.Format(AppResources.WeekSelect, this.CalendarWeek, this.PeriodWeek);
    }
}
