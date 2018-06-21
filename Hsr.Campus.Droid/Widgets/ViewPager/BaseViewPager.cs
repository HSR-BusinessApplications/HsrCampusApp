// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets.ViewPager
{
    using System.Collections;
    using System.Windows.Input;
    using Android.Content;
    using Android.Util;
    using MvvmCross.Binding.Attributes;
    using MvvmCross.Binding.Droid.Views;

    public class BaseViewPager<TItemView> : Android.Support.V4.View.ViewPager
        where TItemView : BaseItemView
    {
        private ICommand itemPageSelectedField;
        private ICommand pageSelectedField;
        private bool pageSelectedOverloaded;
        private bool itemPageSelectedOverloaded;

        public BaseViewPager(Context context, IAttributeSet attrs)
            : this(context, attrs, new BindablePagerAdapter<TItemView>(context))
        {
        }

        public BaseViewPager(Context context, IAttributeSet attrs, BindablePagerAdapter<TItemView> adapter)
            : base(context, attrs)
        {
            var itemTemplateId = MvxAttributeHelpers.ReadListItemTemplateId(context, attrs);
            adapter.ItemTemplateId = itemTemplateId;
            this.Adapter = adapter;
        }

        public new ICommand PageSelected
        {
            get
            {
                return this.pageSelectedField;
            }

            set
            {
                this.pageSelectedField = value;
                if (this.pageSelectedField != null)
                {
                    this.EnsurePageSelectedOverloaded();
                }
            }
        }

        public new BindablePagerAdapter<TItemView> Adapter
        {
            get
            {
                return base.Adapter as BindablePagerAdapter<TItemView>;
            }

            set
            {
                var existing = this.Adapter;
                if (existing == value)
                {
                    return;
                }

                if (existing != null && value != null)
                {
                    value.ItemsSource = existing.ItemsSource;
                    value.ItemTemplateId = existing.ItemTemplateId;
                }

                base.Adapter = value;
            }
        }

        [MvxSetToNullAfterBinding]
        public IEnumerable ItemsSource
        {
            get { return this.Adapter.ItemsSource; }
            set { this.Adapter.ItemsSource = value; }
        }

        public int ItemTemplateId
        {
            get { return this.Adapter.ItemTemplateId; }
            set { this.Adapter.ItemTemplateId = value; }
        }

        public ICommand ItemPageSelected
        {
            get
            {
                return this.itemPageSelectedField;
            }

            set
            {
                this.itemPageSelectedField = value;
                if (this.itemPageSelectedField != null)
                {
                    this.EnsureItemPageSelectedOverloaded();
                }
            }
        }

        private void EnsureItemPageSelectedOverloaded()
        {
            if (this.itemPageSelectedOverloaded)
            {
                return;
            }

            this.itemPageSelectedOverloaded = true;
            base.PageSelected += (sender, args) => this.ExecuteCommandOnItem(this.ItemPageSelected, args.Position);
        }

        private void ExecuteCommandOnItem(ICommand command, int position)
        {
            if (command == null)
            {
                return;
            }

            var item = this.Adapter.GetRawItem(position);
            if (item == null)
            {
                return;
            }

            if (!command.CanExecute(item))
            {
                return;
            }

            command.Execute(item);
        }

        private void EnsurePageSelectedOverloaded()
        {
            if (this.pageSelectedOverloaded)
            {
                return;
            }

            this.pageSelectedOverloaded = true;
            base.PageSelected += (sender, args) => this.ExecuteCommand(this.PageSelected, args.Position);
        }

        private void ExecuteCommand(ICommand command, int toPage)
        {
            if (command == null)
            {
                return;
            }

            if (!command.CanExecute(toPage))
            {
                return;
            }

            command.Execute(toPage);
        }
    }
}
