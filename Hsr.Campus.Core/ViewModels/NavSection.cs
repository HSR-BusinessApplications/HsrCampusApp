// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System.Collections.ObjectModel;

    public class NavSection
        : ITitled
    {
        public string Title { get; set; }

        public ObservableCollection<NavItem> Items { get; set; }
    }
}
