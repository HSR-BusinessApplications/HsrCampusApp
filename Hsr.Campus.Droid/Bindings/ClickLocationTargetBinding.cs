// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Bindings
{
    using System;
    using System.Windows.Input;
    using Hsr.Campus.Droid.Widgets;
    using MvvmCross.Binding;
    using MvvmCross.Binding.Droid.Target;

    public class ClickLocationTargetBinding : MvxAndroidTargetBinding
    {
        private ICommand command;

        public ClickLocationTargetBinding(TouchImageView target)
            : base(target)
        {
            target.ClickLoction += this.Target_ClickLoction;
        }

        public override Type TargetType => typeof(ICommand);

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected TouchImageView ListView => (TouchImageView)this.Target;

        protected override void SetValueImpl(object target, object value)
        {
            if (value == null)
            {
                return;
            }

            this.command = value as ICommand;
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                var view = this.ListView;
                if (view != null)
                {
                    view.ClickLoction -= this.Target_ClickLoction;
                }
            }

            base.Dispose(isDisposing);
        }

        private void Target_ClickLoction(object sender, LocationEventArgs e)
        {
            if (this.command == null)
            {
                return;
            }

            if (!this.command.CanExecute(e.Point))
            {
                return;
            }

            this.command.Execute(e.Point);
        }
    }
}
