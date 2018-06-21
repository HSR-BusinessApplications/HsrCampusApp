// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System.Collections.ObjectModel;
    using Model;
    using MvvmCross.Core.ViewModels;

    public class ExamEntriesViewModel : MvxViewModel, ITitled
    {
        public string Name
        {
            get; set;
        }

        public ObservableCollection<SqlAdunisAppointment> Entries
        {
            get; set;
        }

        public string Title
        {
            get { return this.Name; }
            set { this.Name = value; }
        }
    }
}
