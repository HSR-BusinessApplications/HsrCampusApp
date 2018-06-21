// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets.ViewPager
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using Android.Content;
    using Android.Support.V4.View;
    using Android.Views;
    using Android.Widget;
    using Core.ViewModels;
    using Java.Lang;
    using MvvmCross.Binding;
    using MvvmCross.Binding.Attributes;
    using MvvmCross.Binding.Droid.BindingContext;
    using MvvmCross.Binding.ExtensionMethods;
    using MvvmCross.Platform.Exceptions;
    using MvvmCross.Platform.Platform;
    using MvvmCross.Platform.WeakSubscription;
    using IList = System.Collections.IList;

    public class BindablePagerAdapter<TItemView> : PagerAdapter
        where TItemView : BaseItemView
    {
        private int itemTemplateIdField;
        private IEnumerable itemsSourceField;
        private IDisposable subscription;

        public BindablePagerAdapter(Context context)
            : this(context, MvxAndroidBindingContextHelpers.Current())
        {
        }

        public BindablePagerAdapter(Context context, IMvxAndroidBindingContext bindingContext)
        {
            this.Context = context;
            this.BindingContext = bindingContext;
            if (this.BindingContext == null)
            {
                throw new MvxException(
                    "BindablePagerAdapter can only be used within a Context which supports IMvxBindingActivity");
            }

            this.SimpleViewLayoutId = Android.Resource.Layout.SimpleListItem1;
            this.ReloadAllOnDataSetChange = true; // default is to reload all
        }

        public bool ReloadAllOnDataSetChange { get; set; }

        public int SimpleViewLayoutId { get; set; }

        [MvxSetToNullAfterBinding]
        public IEnumerable ItemsSource
        {
            get { return this.itemsSourceField; }
            set { this.SetItemsSource(value); }
        }

        public int ItemTemplateId
        {
            get
            {
                return this.itemTemplateIdField;
            }

            set
            {
                if (this.itemTemplateIdField == value)
                {
                    return;
                }

                this.itemTemplateIdField = value;
                if (this.itemsSourceField != null)
                {
                    this.NotifyDataSetChanged();
                }
            }
        }

        public override int Count => this.itemsSourceField.Count();

        protected Context Context { get; }

        protected IMvxAndroidBindingContext BindingContext { get; }

        public int GetPosition(object item) => this.itemsSourceField.GetPosition(item);

        public object GetRawItem(int position) => this.itemsSourceField.ElementAt(position);

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            ITitled titled;
            try
            {
                titled = this.GetRawItem(position) as ITitled;
            }
            catch (System.Exception)
            {
                return new Java.Lang.String("Invalid");
            }

            return titled == null ? new Java.Lang.String("Invalid") : new Java.Lang.String(titled.Title);
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            var view = this.GetView(position, null, this.ResolveItemTemplateId(position));

            container.AddView(view);

            return view;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            var view = (View)@object;
            container.RemoveView(view);
            view.Dispose();
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object @object) => view == @object;

        // this as a simple non-performant fix for non-updating views - see http://stackoverflow.com/a/7287121/373321
        public override int GetItemPosition(Java.Lang.Object @object)
        {
            return this.ReloadAllOnDataSetChange ? PositionNone : base.GetItemPosition(@object);
        }

        protected virtual int ResolveItemTemplateId(int position) => this.ItemTemplateId > 0 ? this.ItemTemplateId : 0;

        protected virtual void NotifyDataSetChanged(NotifyCollectionChangedEventArgs e)
        {
            this.NotifyDataSetChanged();
        }

        protected virtual void SetItemsSource(IEnumerable value)
        {
            if (Equals(this.itemsSourceField, value))
            {
                return;
            }

            if (this.subscription != null)
            {
                this.subscription.Dispose();
                this.subscription = null;
            }

            this.itemsSourceField = value;
            if (this.itemsSourceField != null && !(this.itemsSourceField is IList))
            {
                MvxBindingTrace.Trace(
                    MvxTraceLevel.Warning,
                    "Binding to IEnumerable rather than IList - this can be inefficient, especially for large lists");
            }

            var newObservable = this.itemsSourceField as INotifyCollectionChanged;
            if (newObservable != null)
            {
                this.subscription = newObservable.WeakSubscribe(this.OnItemsSourceCollectionChanged);
            }

            this.NotifyDataSetChanged();
        }

        protected virtual View GetSimpleView(View convertView, object source)
        {
            if (convertView == null)
            {
                convertView = this.CreateSimpleView(source);
            }
            else
            {
                this.BindSimpleView(convertView, source);
            }

            return convertView;
        }

        protected virtual void BindSimpleView(View convertView, object source)
        {
            var textView = convertView as TextView;
            if (textView != null)
            {
                textView.Text = (source ?? string.Empty).ToString();
            }
        }

        protected virtual View CreateSimpleView(object source)
        {
            var view = this.BindingContext.LayoutInflaterHolder.LayoutInflater.Inflate(this.SimpleViewLayoutId, null);
            this.BindSimpleView(view, source);
            return view;
        }

        protected virtual View GetBindableView(View convertView, object source) => this.GetBindableView(convertView, source, this.ItemTemplateId);

        protected virtual View GetBindableView(View convertView, object source, int templateId)
        {
            if (templateId == 0)
            {
                // no template seen - so use a standard string view from Android and use ToString()
                return this.GetSimpleView(convertView, source);
            }

            // we have a templateid so lets use bind and inflate on it :)
            var viewToUse = convertView as TItemView;
            if (viewToUse != null && viewToUse.TemplateId != templateId)
            {
                viewToUse = null;
            }

            if (viewToUse == null)
            {
                viewToUse = this.CreateBindableView(source, templateId);
            }
            else
            {
                this.BindBindableView(source, viewToUse);
            }

            return viewToUse.Content;
        }

        protected virtual void BindBindableView(object source, TItemView viewToUse)
        {
            viewToUse.DataContext = source;
        }

        protected virtual TItemView CreateBindableView(object dataContext, int templateId) => CreateItemView(this.Context, this.BindingContext.LayoutInflaterHolder, dataContext, null, templateId);

        private static TItemView CreateItemView(params object[] args)
        {
            return (TItemView)Activator.CreateInstance(typeof(TItemView), args);
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.NotifyDataSetChanged(e);
        }

        private View GetView(int position, View convertView, int templateId)
        {
            if (this.itemsSourceField == null)
            {
                MvxBindingTrace.Trace(MvxTraceLevel.Error, "GetView called when ItemsSource is null");
                return null;
            }

            var source = this.GetRawItem(position);

            return this.GetBindableView(convertView, source, templateId);
        }
    }
}
