// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Linq;
    using Core.Resources;
    using Core.ViewModels;
    using CoreGraphics;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Binding.iOS.Views;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using UIKit;

    [MvxFromStoryboard("Timetable")]
    [MvxChildPresentation]
    internal partial class TimetableViewController : MvxTabBarViewController<TimetableViewModel>
    {
        private int tabCount;

        public TimetableViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.CustomizableViewControllers = new UIViewController[] { };

            this.NavigationItem.Title = AppResources.TileTimetable;

            this.ViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName != "AllDays")
                {
                    return;
                }

                this.LoadTabs();
            };
            this.LoadTabs();

            var selectWeek = new UIBarButtonItem(AppResources.WeekSelection, UIBarButtonItemStyle.Plain, (o, e) =>
            {
                var alertController = UIAlertController.Create(AppResources.WeekSelection, "\n\n\n\n\n\n\n\n\n\n\n", UIAlertControllerStyle.ActionSheet);

                var picker = new UIPickerView(new CGRect(0, 35, 300, 215));

                var pickerViewModel = new MvxPickerViewModel(picker);
                picker.Model = pickerViewModel;
                picker.ShowSelectionIndicator = true;

                var set = this.CreateBindingSet<TimetableViewController, TimetableViewModel>();
                set.Bind(pickerViewModel).For(p => p.ItemsSource).To(vm => vm.Weeks);
                set.Bind(pickerViewModel).For(p => p.SelectedItem).To(vm => vm.CurrentWeek);
                set.Apply();

                alertController.Add(picker);
                alertController.AddAction(UIAlertAction.Create(AppResources.ActionDone, UIAlertActionStyle.Default, t => alertController.DismissViewController(false, null)));

                this.PresentViewController(alertController, true, null);
            });

            this.NavigationItem.SetRightBarButtonItem(selectWeek, false);
        }

        private void LoadTabs()
        {
            if (!this.ViewModel.HasContent)
            {
                return;
            }

            this.ViewControllers = this.ViewModel.AllDays.Select(this.CreateTabFor).ToArray();

            if (!(this.ViewControllers?.Length > 0))
            {
                return;
            }

            var index = 0;
            for (var i = 0; i < this.ViewModel.AllDays.Count; i++)
            {
                if (!this.ViewModel.AllDays[i].Date.HasValue ||
                    this.ViewModel.AllDays[i].Date.Value.DayOfWeek < DateTime.Now.DayOfWeek)
                {
                    continue;
                }

                index = i;
                break;
            }

            this.SelectedViewController = this.ViewControllers[index];
        }

        private UIViewController CreateTabFor(TimetableEntriesViewModel timetableEntriesViewModel)
        {
            var controller = new UINavigationController();
            var screen = this.CreateViewControllerFor(timetableEntriesViewModel) as UIViewController;

            controller.PushViewController(screen, false);

            UIImage image;

            if (timetableEntriesViewModel.Date.HasValue)
            {
                var tabBarImage = string.Empty;
                switch (timetableEntriesViewModel.Date.Value.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        tabBarImage += "Mo";
                        break;
                    case DayOfWeek.Tuesday:
                        tabBarImage += "Di";
                        break;
                    case DayOfWeek.Wednesday:
                        tabBarImage += "Mi";
                        break;
                    case DayOfWeek.Thursday:
                        tabBarImage += "Do";
                        break;
                    case DayOfWeek.Friday:
                        tabBarImage += "Fr";
                        break;
                    case DayOfWeek.Saturday:
                        tabBarImage += "Sa";
                        break;
                    case DayOfWeek.Sunday:
                        tabBarImage += "So";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                image = UIImage.FromBundle(tabBarImage);
                controller.TabBarItem = new UITabBarItem($"{timetableEntriesViewModel.Date.Value:dd.MM.yyyy}", image, this.tabCount++);

                timetableEntriesViewModel.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "Title")
                    {
                        controller.TabBarItem.Title = $"{timetableEntriesViewModel.Date.Value:dd.MM.yyyy}";
                    }
                };

                screen.NavigationItem.Title = "(W" + timetableEntriesViewModel.Date.Value.WeekNr() + ") " + tabBarImage + " - " + $"{timetableEntriesViewModel.Date.Value:dd.MM.yyyy}";
            }
            else
            {
                image = UIImage.FromBundle("Div");
                controller.TabBarItem = new UITabBarItem("Diverse", image, this.tabCount++);

                timetableEntriesViewModel.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "Title")
                    {
                        controller.TabBarItem.Title = "Diverse";
                    }
                };

                screen.NavigationItem.Title = "Diverse";
            }

            return controller;
        }
    }
}
