// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Views;

    public class ProxyViewModelRequest : MvxViewModelInstanceRequest
    {
        public ProxyViewModelRequest(Type viewType, IMvxViewModel viewModel)
            : base(viewModel)
        {
            this.ViewType = viewType;
        }

        public Type ViewType { get; private set; }
    }
}
