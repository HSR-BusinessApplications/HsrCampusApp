// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;
    using ApplicationServices;
    using DomainServices;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using Properties;
    using Resources;

    public class SettingsViewModel
        : AbstractParameterizedViewModel<object>, ITitled
    {
        private readonly IIOCacheService cacheService;
        private readonly IUserInteractionService userInteraction;
        private readonly IAdunisRepository adunisRepository;
        private readonly INewsRepository newsRepository;
        private readonly IMenuRepository menuRepository;
        private readonly IDevice device;

        private string titleField;

        public SettingsViewModel(IMvxNavigationService navigationService, IIOCacheService cacheService, IUserInteractionService userInteraction, IAdunisRepository adunisRepository, INewsRepository newsRepository, IMenuRepository menuRepository, IDevice device)
        {
            this.NavigationService = navigationService;
            this.cacheService = cacheService;
            this.userInteraction = userInteraction;
            this.adunisRepository = adunisRepository;
            this.newsRepository = newsRepository;
            this.menuRepository = menuRepository;
            this.device = device;
        }

        public enum SettingsArgs
        {
            Default = 0,
            Storage,
            Info,
            License
        }

        public ICommand ShowDetailCommand => new MvxCommand<Setting>(s => s.Act?.Invoke());

        public ObservableCollection<Setting> Settings { get; set; }

        public string Title
        {
            get
            {
                return this.titleField;
            }

            set
            {
                this.titleField = value;
                this.RaisePropertyChanged();
            }
        }

        public void Init(SettingsArgs args = SettingsArgs.Default)
        {
            this.Settings = new ObservableCollection<Setting>();
            this.Settings.CollectionChanged += (e, _) => this.RaisePropertyChanged(nameof(this.Settings));

            switch (args)
            {
                case SettingsArgs.Default:

                    this.Title = AppResources.Settings;

                    this.Settings.Add(new Setting { Act = () => this.Navigate<AccountViewModel, AccountViewModel.Args>(new AccountViewModel.Args { Mode = AccountViewModel.Mode.View }), Image = "InfoAccount.png", Title = AppResources.SettingAccountTitle, SubTitle = AppResources.SettingAccountTitleSub });

                    if (this.device.Platform != DevicePlatform.iOS)
                    {
                        this.Settings.Add(new Setting { Act = () => this.Navigate<SettingsViewModel, object>(new { args = SettingsArgs.Storage }), Image = "InfoStorage.png", Title = AppResources.SettingStorageTitle, SubTitle = AppResources.SettingStorageTitleSub });
                    }

                    this.Settings.Add(new Setting { Act = () => this.Navigate<SettingsViewModel, object>(new { args = SettingsArgs.Info }), Image = "InfoHeart.png", Title = AppResources.SettingAppInfoTitle, SubTitle = AppResources.SettingAppInfoSubTitle });

                    break;

                case SettingsArgs.Info:

                    this.Title = AppResources.SettingAppInfoTitle;

                    this.Settings.Add(new Setting
                    {
                        Title = AppResources.AppVersionTitle,
                        SubTitle = $"{AssemblyInfo.AssemblyInformationalVersion}"
                    });
                    this.Settings.Add(new Setting { Title = AppResources.SettingOfficialTitle, SubTitle = AppResources.SettingOfficialTitleSub });
                    this.Settings.Add(new Setting { Title = AppResources.AppFirstRunTitle, SubTitle = AppResources.AppFirstRunMessage });
                    this.Settings.Add(new Setting { Act = () => this.Navigate<SettingsViewModel, object>(new { args = SettingsArgs.License }), Title = AppResources.ThirdPartyLibraries, SubTitle = AppResources.ThirdPartyLibrariesText });
                    break;
                case SettingsArgs.Storage:

                    this.Title = AppResources.SettingStorageTitle;

                    var usage = this.cacheService.Usage();

                    this.Settings.Add(new Setting
                    {
                        Title = AppResources.SettingCacheTitle,
                        SubTitle = string.Format(AppResources.SettingCacheTitleSub, usage.FileCount, usage.ByteCount / (1024.0 * 1024.0)),
                        Act = this.ClearCache
                    });

                    break;
                case SettingsArgs.License:

                    this.Title = AppResources.ThirdPartyLibraries;

                    foreach (var licensePair in PublicAppResources.ThirdPartyLicenses)
                    {
                        this.Settings.Add(new Setting { Title = licensePair.Key, SubTitle = licensePair.Value });
                    }

                    break;
                default:
                    Debug.Assert(false, "not all args implemented");
                    break;
            }
        }

        public override void Prepare(object parameter)
        {
        }

        private void ClearCache()
        {
            this.IsUpdating = true;
            this.newsRepository.Truncate();
            this.menuRepository.Truncate();
            this.adunisRepository.Truncate();
            this.cacheService.ClearAll();

            var usage = this.cacheService.Usage();

            this.Settings.First(setting => setting.Title.Equals(AppResources.SettingCacheTitle)).SubTitle = string.Format(AppResources.SettingCacheTitleSub, usage.FileCount, usage.ByteCount / (1024.0 * 1024.0));

            this.userInteraction.Toast(AppResources.SettingSuccessCacheClear, ToastTime.Short);

            this.IsUpdating = false;
        }
    }
}
