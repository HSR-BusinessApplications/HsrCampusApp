// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Test.Mocks
{
    using System;
    using System.Collections.Generic;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Core.Views;
    using MvvmCross.Platform.Core;

    public class MockDispatcher
    : MvxMainThreadDispatcher,
      IMvxViewDispatcher
    {
        public List<MvxViewModelRequest> Requests { get; } = new List<MvxViewModelRequest>();

        public List<MvxPresentationHint> Hints { get; } = new List<MvxPresentationHint>();

        public bool RequestMainThreadAction(Action action, bool maskExceptions = true)
        {
            action();
            return true;
        }

        public bool ShowViewModel(MvxViewModelRequest request)
        {
            this.Requests.Add(request);
            return true;
        }

        public bool ChangePresentation(MvxPresentationHint hint)
        {
            this.Hints.Add(hint);
            return true;
        }
    }
}
