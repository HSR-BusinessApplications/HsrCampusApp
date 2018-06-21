// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets
{
    using System;
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Preferences;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Droid.Views;
    using MvvmCross.Platform.Droid.Views;
    using MvvmCross.Platform.Platform;

    public abstract class AbstractPreferenceView
        : PreferenceActivity, IMvxAndroidView
    {
        private IMvxViewModel viewModelField;

        protected AbstractPreferenceView()
        {
            this.IsVisible = true;
        }

        public event EventHandler<MvxIntentResultEventArgs> MvxIntentResultReceived;

        public bool IsVisible { get; private set; }

        public object DataContext { get; set; }

        public IMvxBindingContext BindingContext { get; set; }

        public IMvxViewModel ViewModel
        {
            get
            {
                return this.viewModelField;
            }

            set
            {
                this.viewModelField = value;
                this.OnViewModelSet();
            }
        }

        public void MvxInternalStartActivityForResult(Intent intent, int requestCode)
        {
            throw new NotImplementedException();
        }

        public override void StartActivityForResult(Intent intent, int requestCode)
        {
            if (requestCode == (int)MvxIntentRequestCode.PickFromFile)
            {
                MvxTrace.Trace(
                    "Warning - activity request code may clash with Mvx code for {0}",
                   (MvxIntentRequestCode)requestCode);
            }

            base.StartActivityForResult(intent, requestCode);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.OnViewCreate(savedInstanceState);
        }

        protected override void OnDestroy()
        {
            this.OnViewDestroy();
            base.OnDestroy();
        }

        protected abstract void OnViewModelSet();

        protected override void OnResume()
        {
            base.OnResume();
            this.IsVisible = true;
            this.OnViewResume();
        }

        protected override void OnPause()
        {
            this.OnViewPause();
            this.IsVisible = false;
            base.OnPause();
        }

        protected override void OnStart()
        {
            base.OnStart();
            this.OnViewStart();
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            this.OnViewRestart();
        }

        protected override void OnStop()
        {
            this.OnViewStop();
            base.OnStop();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            this.MvxIntentResultReceived?.Invoke(this, new MvxIntentResultEventArgs(requestCode, resultCode, data));

            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}
