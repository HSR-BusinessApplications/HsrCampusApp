// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Bindings
{
    using System;
    using System.Windows.Input;
    using Android.Widget;
    using MvvmCross.Binding;
    using MvvmCross.Binding.Droid.Target;
    using MvvmCross.Binding.Droid.Views;

    public class LoadMoreTargetBinding : MvxAndroidTargetBinding
    {
        private ICommand command;

        public LoadMoreTargetBinding(AbsListView target)
            : base(target)
        {
            target.Scroll += this.Target_Scroll;
        }

        public override Type TargetType => typeof(ICommand);

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected MvxListView ListView => (MvxListView)this.Target;

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
                    view.Scroll -= this.Target_Scroll;
                }
            }

            base.Dispose(isDisposing);
        }

        private void Target_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
            if (e.TotalItemCount <= 0 || (e.TotalItemCount - e.FirstVisibleItem >= 4 && e.VisibleItemCount + e.FirstVisibleItem < e.TotalItemCount))
            {
                return;
            }

            if (this.command == null)
            {
                return;
            }

            if (!this.command.CanExecute(null))
            {
                return;
            }

            this.command.Execute(null);
        }
    }
}
