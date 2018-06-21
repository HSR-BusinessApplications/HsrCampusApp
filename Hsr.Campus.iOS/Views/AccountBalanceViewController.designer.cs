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
    [Register ("AccountBalanceViewController")]
    partial class AccountBalanceViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView BadgeText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel BalanceLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LastUpdateLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BadgeText != null) {
                BadgeText.Dispose ();
                BadgeText = null;
            }

            if (BalanceLabel != null) {
                BalanceLabel.Dispose ();
                BalanceLabel = null;
            }

            if (LastUpdateLabel != null) {
                LastUpdateLabel.Dispose ();
                LastUpdateLabel = null;
            }
        }
    }
}
