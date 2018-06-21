// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Hsr.Campus.iOS
{
    [Register ("AccountInfoViewController")]
    partial class AccountInfoViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonOAuth { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonRemove { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelIdentity { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelIdentityText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelRefresh { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelRefreshText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonOAuth != null) {
                ButtonOAuth.Dispose ();
                ButtonOAuth = null;
            }

            if (ButtonRemove != null) {
                ButtonRemove.Dispose ();
                ButtonRemove = null;
            }

            if (LabelIdentity != null) {
                LabelIdentity.Dispose ();
                LabelIdentity = null;
            }

            if (LabelIdentityText != null) {
                LabelIdentityText.Dispose ();
                LabelIdentityText = null;
            }

            if (LabelRefresh != null) {
                LabelRefresh.Dispose ();
                LabelRefresh = null;
            }

            if (LabelRefreshText != null) {
                LabelRefreshText.Dispose ();
                LabelRefreshText = null;
            }
        }
    }
}
