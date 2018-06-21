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
    [Register ("AccountIndexViewController")]
    partial class AccountIndexViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonOAuth { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ContentText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonOAuth != null) {
                ButtonOAuth.Dispose ();
                ButtonOAuth = null;
            }

            if (ContentText != null) {
                ContentText.Dispose ();
                ContentText = null;
            }
        }
    }
}
