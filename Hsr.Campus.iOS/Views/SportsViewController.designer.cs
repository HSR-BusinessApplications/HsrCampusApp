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
    [Register ("SportsViewController")]
    partial class SportsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSportsAgenda { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonSportsAgenda != null) {
                ButtonSportsAgenda.Dispose ();
                ButtonSportsAgenda = null;
            }

            if (LabelText != null) {
                LabelText.Dispose ();
                LabelText = null;
            }
        }
    }
}