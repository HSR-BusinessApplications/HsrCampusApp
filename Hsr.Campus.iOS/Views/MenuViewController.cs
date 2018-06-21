// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.ComponentModel;
    using Core.Model;
    using Core.Resources;
    using Core.ViewModels;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using UIKit;
    using Utils;

    [MvxFromStoryboard("Menu")]
    [MvxChildPresentation]
    internal partial class MenuViewController : MvxViewController<MenuViewModel>
    {
        public MenuViewController(IntPtr handle)
            : base(handle)
        {
        }

        private SqlMenu Menu { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (this.ViewModel == null)
            {
                return;
            }

            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("Back", UIBarButtonItemStyle.Plain, null);

            this.NavigationItem.Title = AppResources.TileMenu;
            this.SetRightBarItem(this.ViewModel.UpdateCommand);

            this.ViewModel.PropertyChanged += this.ViewModel_PropertyChanged;
            this.FeedSelection.ValueChanged += this.FeedSelection_ValueChanged;
            this.Backward.PrimaryActionTriggered += this.BackwardOnPrimaryActionTriggered;
            this.Forward.PrimaryActionTriggered += this.ForwardOnPrimaryActionTriggered;

            this.NoContent.Text = AppResources.NoData;
            this.NoMenu.Text = AppResources.NoMenus;

            this.SetupView();
        }

        private void BackwardOnPrimaryActionTriggered(object sender, EventArgs eventArgs)
        {
            var newIndex = this.ViewModel.SelectedFeedVm.Items.IndexOf(this.Menu) - 1;
            if (newIndex < 0)
            {
                return;
            }

            this.ChangeSelectedDay(this.ViewModel.SelectedFeedVm.Items[newIndex].Date);
            (this.DateInput.InputView as UIPickerView)?.Select(newIndex, 0, true);
        }

        private void ForwardOnPrimaryActionTriggered(object sender, EventArgs eventArgs)
        {
            var newIndex = this.ViewModel.SelectedFeedVm.Items.IndexOf(this.Menu) + 1;
            if (newIndex >= this.ViewModel.SelectedFeedVm.Items.Count)
            {
                return;
            }

            this.ChangeSelectedDay(this.ViewModel.SelectedFeedVm.Items[newIndex].Date);
            (this.DateInput.InputView as UIPickerView)?.Select(newIndex, 0, true);
        }

        private void SetupView()
        {
            if (this.ViewModel.Items.Count > 0)
            {
                this.NoContent.Hidden = true;
                this.WebView.Hidden = false;
                this.DateInput.Hidden = false;
                this.Backward.Hidden = false;
                this.Forward.Hidden = false;

                this.FeedSelection.RemoveAllSegments();

                for (var i = 0; i < this.ViewModel.Items.Count; i++)
                {
                    this.FeedSelection.InsertSegment(this.ViewModel.Items[i].Title, i, false);
                }

                this.SetupFeedView();
            }
            else
            {
                this.NoContent.Hidden = false;
                this.WebView.Hidden = true;
                this.DateInput.Hidden = true;
                this.Backward.Hidden = true;
                this.Forward.Hidden = true;
            }
        }

        private void SetupFeedView()
        {
            if (this.ViewModel.SelectedFeedVm == null)
            {
                this.NoMenu.Hidden = true;
                this.FeedName.Text = string.Empty;
                return;
            }

            this.FeedSelection.SelectedSegment = this.ViewModel.Items.IndexOf(this.ViewModel.SelectedFeedVm);

            this.FeedName.Text = this.ViewModel.SelectedFeedVm.Feed.Name;

            if (this.ViewModel.SelectedFeedVm.Items.Count > 0)
            {
                this.NoMenu.Hidden = true;

                this.Menu = this.ViewModel.SelectedFeedVm.FindItemClosestToSelectedDay();

                var index = this.ViewModel.SelectedFeedVm.Items.IndexOf(this.Menu);
                this.Backward.Enabled = index != 0;
                this.Forward.Enabled = index != this.ViewModel.SelectedFeedVm.Items.Count - 1;

                this.DateInput.Enabled = true;
                this.DateInput.Text = this.Menu.Title;

                var datePickerModel = new DatePickerModel(this.ViewModel.SelectedFeedVm.Items);
                datePickerModel.PickerChanged += this.Model_PickerChanged;

                var dateInputInputView = new UIPickerView
                {
                    Model = datePickerModel
                };

                dateInputInputView.Select(index, 0, false);

                this.DateInput.InputView = dateInputInputView;

                this.LoadWebView();
            }
            else
            {
                this.NoMenu.Hidden = false;

                this.DateInput.Enabled = false;
                this.DateInput.Text = string.Empty;

                this.Backward.Enabled = false;
                this.Forward.Enabled = false;

                this.WebView.LoadHtmlString(string.Empty, null);
            }
        }

        private void ChangeSelectedDay(DateTime newDay)
        {
            this.ViewModel.SelectedDay = newDay;
            this.Menu = this.ViewModel.SelectedFeedVm.FindItemClosestToSelectedDay();

            this.DateInput.Text = this.Menu.Title;

            var index = this.ViewModel.SelectedFeedVm.Items.IndexOf(this.Menu);
            this.Backward.Enabled = index != 0;
            this.Forward.Enabled = index != this.ViewModel.SelectedFeedVm.Items.Count - 1;

            this.LoadWebView();
        }

        private void LoadWebView()
        {
            if (this.Menu == null)
            {
                return;
            }

            this.WebView.LoadHtmlString(this.Menu.HtmlPage, null);
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MenuViewModel.Items))
            {
                this.SetupView();
                return;
            }

            if (e.PropertyName == nameof(MenuViewModel.SelectedFeedVm))
            {
                this.SetupFeedView();
            }
        }

        private void FeedSelection_ValueChanged(object sender, EventArgs e)
        {
            if (sender is UISegmentedControl uiSegmentedControl)
            {
                var index = (int)uiSegmentedControl.SelectedSegment;
                this.ViewModel.SelectedFeedVm = this.ViewModel.Items[index];
            }
        }

        private void Model_PickerChanged(object sender, DatePickerChangedEventArgs e)
        {
            this.ChangeSelectedDay(e.SelectedValue.Date);
        }
    }
}
