// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Collections.Specialized;
    using System.Windows.Input;
    using Hsr.Campus.iOS.PlatformServices;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Views;
    using UIKit;

    public class LinkerPleaseInclude
    {
        /// <summary>
        /// Includes the specified UI button.
        /// </summary>
        /// <param name="uiButton">The UI button.</param>
        public void Include(UIButton uiButton)
        {
            uiButton.TouchUpInside += (s, e) => uiButton.SetTitle(uiButton.Title(UIControlState.Normal), UIControlState.Normal);
        }

        /// <summary>
        /// Includes the specified bar button.
        /// </summary>
        /// <param name="barButton">The bar button.</param>
        public void Include(UIBarButtonItem barButton)
        {
            barButton.Enabled = !barButton.Enabled;
            barButton.Clicked += (s, e) => barButton.Title += string.Empty;
        }

        /// <summary>
        /// Includes the specified text field.
        /// </summary>
        /// <param name="textField">The text field.</param>
        public void Include(UITextField textField)
        {
            textField.Text += string.Empty;
            textField.EditingChanged += (sender, args) => textField.Text = string.Empty;
        }

        /// <summary>
        /// Includes the specified text view.
        /// </summary>
        /// <param name="uiTextView">The text view.</param>
        public void Include(UITextView uiTextView)
        {
            uiTextView.Text += string.Empty;
            uiTextView.Changed += (sender, args) => uiTextView.Text = string.Empty;
        }

        /// <summary>
        /// Includes the specified uiLabel.
        /// </summary>
        /// <param name="uiLabel">The uiLabel.</param>
        public void Include(UILabel uiLabel)
        {
            uiLabel.Text = uiLabel.Text;
            uiLabel.TextColor = Constants.HsrBlue;
            uiLabel.Hidden = !uiLabel.Hidden;
        }

        /// <summary>
        /// Includes the specified image view.
        /// </summary>
        /// <param name="uiImageView">The image view.</param>
        public void Include(UIImageView uiImageView)
        {
            uiImageView.Image = new UIImage(uiImageView.Image.CIImage);
        }

        /// <summary>
        /// Includes the specified uiDatePicker.
        /// </summary>
        /// <param name="uiDatePicker">The uiDatePicker.</param>
        public void Include(UIDatePicker uiDatePicker)
        {
            uiDatePicker.Date = uiDatePicker.Date.AddSeconds(1);
            uiDatePicker.ValueChanged += (sender, args) => uiDatePicker.Date = (Foundation.NSDate)DateTime.MaxValue;
        }

        /// <summary>
        /// Includes the specified uiSlider.
        /// </summary>
        /// <param name="uiSlider">The uiSlider.</param>
        public void Include(UISlider uiSlider)
        {
            uiSlider.Value++;
            uiSlider.ValueChanged += (sender, args) => uiSlider.Value = 1;
        }

        /// <summary>
        /// Includes the specified UI switch.
        /// </summary>
        /// <param name="uiSwitch">The UI switch.</param>
        public void Include(UISwitch uiSwitch)
        {
            uiSwitch.On = !uiSwitch.On;
            uiSwitch.ValueChanged += (sender, args) => uiSwitch.On = false;
        }

        /// <summary>
        /// Includes the specified View Controller
        /// </summary>
        /// <param name="vc">The View Controller.</param>
        public void Include(MvxViewController vc)
        {
            vc.Title += string.Empty;
        }

        /// <summary>
        /// Includes the specified Stepper
        /// </summary>
        /// <param name="s">The Stepper.</param>
        public void Include(UIStepper s)
        {
            s.Value++;
            s.ValueChanged += (sender, args) => s.Value = 0;
        }

        /// <summary>
        /// Includes the specified Page Control
        /// </summary>
        /// <param name="s">The Page Control.</param>
        public void Include(UIPageControl s)
        {
            s.Pages += 1;
            s.ValueChanged += (sender, args) => s.Pages = 0;
        }

        /// <summary>
        /// Includes the specified changed.
        /// </summary>
        /// <param name="changed">The changed.</param>
        public void Include(INotifyCollectionChanged changed)
        {
            changed.CollectionChanged += (s, e) =>
            {
                var test = $"{e.Action}{e.NewItems}{e.NewStartingIndex}{e.OldItems}{e.OldStartingIndex}";
            };
        }

        /// <summary>
        /// Includes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
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

        /// <summary>
        /// Includes the specified Property Injector.
        /// </summary>
        /// <param name="injector">The Property Injector.</param>
        public void Include(MvvmCross.Platform.IoC.MvxPropertyInjector injector)
        {
            injector = new MvvmCross.Platform.IoC.MvxPropertyInjector();
        }

        /// <summary>
        /// Includes the specified INotifyPropertyChanged.
        /// </summary>
        /// <param name="changed">The INotifyPropertyChanged Object.</param>
        public void Include(System.ComponentModel.INotifyPropertyChanged changed)
        {
            changed.PropertyChanged += (sender, e) => { var test = e.PropertyName; };
        }

        /// <summary>
        /// Includes the specified NavigationService and ViewModelLoader.
        /// </summary>
        /// <param name="service">The NavigationService.</param>
        /// <param name="loader">The ViewModelLoader.</param>
        public void Include(MvxNavigationService service, IMvxViewModelLoader loader)
        {
            service = new MvxNavigationService(null, loader);
        }

        /// <summary>
        /// Includes the specified ConsoleColor.
        /// </summary>
        /// <param name="color">The Console Color.</param>
        public void Include(ConsoleColor color)
        {
            Console.Write(string.Empty);
            Console.WriteLine(string.Empty);
            color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }
    }
}
