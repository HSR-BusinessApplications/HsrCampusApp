// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid
{
    using System.Collections.Specialized;
    using System.Windows.Input;
    using Android.Views;
    using Android.Widget;
    using Hsr.Campus.Droid.Widgets;
    using MvvmCross.Binding.Droid.Views;

    // This class is never actually executed, but when Xamarin linking is enabled it does how to ensure types and properties
    // are preserved in the deployed app
    public class LinkerPleaseInclude
    {
        public void Include(Button button)
        {
            button.Click += (s, e) => button.Text += string.Empty;
        }

        public void Include(CheckBox checkBox)
        {
            checkBox.CheckedChange += (sender, args) => checkBox.Checked = !checkBox.Checked;
        }

        public void Include(View view)
        {
            view.Click += (s, e) => view.ContentDescription += string.Empty;
        }

        public void IncludeVisibility(View view)
        {
            view.Visibility += 1;
        }

        public void IncludeRelativeLayout(RelativeLayout relative)
        {
            relative.Visibility = ViewStates.Visible;
        }

        public void IncludeHref(WebButtonView btn)
        {
            btn.Href += string.Empty;
        }

        public void Include(TouchImageView view)
        {
            view.ClickLoction += (sender, args) => view.ContentDescription += string.Empty;
        }

        public void Include(MvxListView list)
        {
            list.Scroll += (s, e) => list.ContentDescription += string.Empty;
        }

        public void Include(TextView text)
        {
            text.TextChanged += (sender, args) => text.Text = string.Empty + text.Text;
            text.Hint = string.Empty + text.Hint;
        }

        public void Include(CompoundButton cb)
        {
            cb.CheckedChange += (sender, args) => cb.Checked = !cb.Checked;
        }

        public void Include(SeekBar sb)
        {
            sb.ProgressChanged += (sender, args) => sb.Progress += 1;
        }

        public void Include(INotifyCollectionChanged changed)
        {
            changed.CollectionChanged += (s, e) =>
            {
                string test = $"{e.Action}{e.NewItems}{e.NewStartingIndex}{e.OldItems}{e.OldStartingIndex}";
            };
        }

        public void Include(ICommand command)
        {
            command.CanExecuteChanged += (s, e) =>
            {
                if (command.CanExecute(null))
                {
                    command.Execute(null);
                }
            };
        }
    }
}
