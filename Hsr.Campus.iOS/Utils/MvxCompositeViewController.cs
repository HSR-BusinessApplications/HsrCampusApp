// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Views;
    using UIKit;

    public class MvxCompositeViewController<TViewModel>
        : MvxViewController
        where TViewModel : IMvxViewModel
    {
        private Dictionary<Predicate<TViewModel>, Type> children;

        public MvxCompositeViewController()
        {
            this.children = new Dictionary<Predicate<TViewModel>, Type>();
        }

        public MvxCompositeViewController(IntPtr handle)
            : base(handle)
        {
            this.children = new Dictionary<Predicate<TViewModel>, Type>();
        }

        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public void AddChildViewController<T>(Predicate<TViewModel> when)
            where T : IMvxIosView
        {
            this.children.Add(when, typeof(T));
        }

        public void RefreshChildView()
        {
            var pair = this.children.FirstOrDefault(t => t.Key(this.ViewModel));

            if (pair.Value == null)
            {
                throw new ArgumentNullException("pair.Value", "No ViewController specified.");
            }

            var view = this.CreateViewControllerFor(new ProxyViewModelRequest(pair.Value, this.ViewModel));

            var controller = (UIViewController)view;

            var attr = pair.Value.GetCustomAttributes(false).OfType<WrapWithAttribute>().FirstOrDefault();

            if (attr != null && attr.With == WrapWithController.NavigationController)
            { // 64
                var point = new CoreGraphics.CGPoint(0, 64);
                var size = new CoreGraphics.CGSize(controller.View.Frame.Size.Width, controller.View.Frame.Size.Height - point.Y);

                controller.View.Frame = new CoreGraphics.CGRect(point, size);
            }

            this.AddChildViewController(controller);

            this.View.AddSubview(controller.View);

            controller.DidMoveToParentViewController(this);
        }
    }
}
