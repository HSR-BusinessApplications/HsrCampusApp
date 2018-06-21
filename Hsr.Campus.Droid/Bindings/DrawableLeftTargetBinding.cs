// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Bindings
{
    using System;
    using Android.Widget;
    using MvvmCross.Binding.Droid.Target;

    public class DrawableLeftTargetBinding
        : MvxAndroidTargetBinding
    {
        public DrawableLeftTargetBinding(TextView target)
            : base(target)
        {
        }

        public override Type TargetType => typeof(int);

        protected TextView TextV => (TextView)this.Target;

        protected override void SetValueImpl(object target, object value)
        {
            if (value == null)
            {
                return;
            }

            this.TextV.SetCompoundDrawablesWithIntrinsicBounds((int)value, 0, 0, 0);
        }
    }
}
