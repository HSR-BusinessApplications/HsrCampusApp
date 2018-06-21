// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Linq;
    using MvvmCross.iOS.Views;
    using UIKit;

    public class MvxStoryBoardViewsContainer : MvxIosViewsContainer
    {
        public override IMvxIosView CreateViewOfType(Type viewType, MvvmCross.Core.ViewModels.MvxViewModelRequest request)
        {
            var requestedType = viewType;

            if (request is ProxyViewModelRequest viewModelRequest)
            {
                requestedType = viewModelRequest.ViewType;
            }

            if (viewType == typeof(HomeViewController))
            {
                return base.CreateViewOfType(requestedType, request);
            }

            try
            {
                var attr = requestedType.GetCustomAttributes(false).OfType<MvxFromStoryboardAttribute>().FirstOrDefault();

                var storyboard = UIStoryboard.FromName(attr == null ? "Home" : attr.StoryboardName, null); // the actual file name of storyboard ( may be different from .cs file)

                return (IMvxIosView)storyboard.InstantiateViewController(requestedType.Name);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                Console.WriteLine("request: " + (request == null));
                Console.WriteLine("requestedType: " + (requestedType == null));
                return base.CreateViewOfType(requestedType, request);
            }
        }
    }
}
