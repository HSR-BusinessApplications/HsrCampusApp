// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets
{
    using System;
    using Core.ViewModels;

    public class LocationEventArgs : EventArgs
    {
        public MapViewModel.Point Point { get; set; }
    }
}
