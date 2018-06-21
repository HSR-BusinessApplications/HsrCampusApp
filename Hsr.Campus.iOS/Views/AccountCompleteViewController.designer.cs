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
    [Register ("AccountCompleteViewController")]
    partial class AccountCompleteViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonHome { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ContentText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonHome != null) {
                ButtonHome.Dispose ();
                ButtonHome = null;
            }

            if (ContentText != null) {
                ContentText.Dispose ();
                ContentText = null;
            }
        }
    }
}
