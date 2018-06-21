// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Bindings
{
    using System;
    using Hsr.Campus.Droid.Widgets;
    using MvvmCross.Binding;
    using MvvmCross.Binding.Droid.Target;

    public class HrefViewTargetBinding : MvxAndroidTargetBinding
    {
        public HrefViewTargetBinding(WebButtonView target)
            : base(target)
        {
        }

        public override Type TargetType => typeof(string);

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected WebButtonView ImageView => (WebButtonView)this.Target;

        protected override void SetValueImpl(object target, object value)
        {
            if (value == null)
            {
                return;
            }

            var btn = (WebButtonView)target;

            btn.Href = (string)value; // boxing due to java string
        }
    }
}
