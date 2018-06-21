// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;
    using ApplicationServices;
    using Hsr.Campus.Core.DomainServices;
    using Hsr.Campus.Core.Model;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using Plugin.Settings.Abstractions;
    using Resources;

    public class HomeViewModel : AbstractViewModel
    {
        private static readonly string FirstRunKey = typeof(HomeViewModel).FullName + "FIRST_RUN";
        private static readonly string TtSyncDoneKey = typeof(HomeViewModel).FullName + "TT_SYNC_DONE";

        private readonly ISettings settings;
        private readonly IUserInteractionService userInteraction;
        private readonly IAccountService account;

        private readonly IAdunisRepository adunisRepository;
        private ObservableCollection<NavSection> items;

        public HomeViewModel(IMvxNavigationService navigationService, ISettings settings, IUserInteractionService userInteraction, IAccountService account, IAdunisRepository adunisRepository)
        {
            this.NavigationService = navigationService;
            this.settings = settings;
            this.userInteraction = userInteraction;
            this.account = account;
            this.adunisRepository = adunisRepository;
        }

        public ICommand GoMenu => new MvxCommand(this.Navigate<MenuViewModel>);

        public ICommand GoAccountBalance => new MvxCommand(this.Navigate<AccountBalanceViewModel>);

        public ICommand GoFiler => new MvxCommand(() => this.Navigate<FilerViewModel, FilerViewModel.FilerArgs>(new FilerViewModel.FilerArgs { CurrentDirectory = string.Empty }));

        public ICommand GoNews => new MvxCommand(this.Navigate<NewsViewModel>);

        public ICommand GoAccount => new MvxCommand(this.Navigate<AccountViewModel>);

        public ICommand GoAdunis => new MvxCommand(this.Navigate<AdunisViewModel>);

        public ICommand GoSports => new MvxCommand(this.Navigate<SportsViewModel>);

        public ICommand GoMap => new MvxCommand(this.Navigate<MapViewModel>);

        public ICommand GoSettings => new MvxCommand(() => this.Navigate<SettingsViewModel, object>(SettingsViewModel.SettingsArgs.Default));

        public bool IsAuth => this.account.HasAccount;

        public ObservableCollection<NavSection> Items
        {
            get
            {
                return this.items;
            }

            set
            {
                this.items = value;
                this.RaisePropertyChanged();
            }
        }

        public ICommand ShowDetailCommand => new MvxCommand<NavItem>(t => t.Detail());

        public void Init()
        {
            this.Items = new ObservableCollection<NavSection>
            {
                new NavSection
                {
                    Title = AppResources.ApplicationTitle,
                    Items = new ObservableCollection<NavItem>
                    {
                        new NavItem
                        {
                            Title = AppResources.TileNews,
                            Detail = this.Navigate<NewsViewModel>,
                            IconPath = "News.png"
                        },
                        new NavItem
                        {
                            Title = AppResources.TileMenu,
                            Detail = this.Navigate<MenuViewModel>,
                            IconPath = "Cafeteria.png"
                        },
                        new NavItem
                        {
                            Title = AppResources.TileCampusMap,
                            Detail = this.Navigate<MapViewModel>,
                            IconPath = "Compass.png"
                        },
                        new NavItem
                        {
                            Title = AppResources.TileSport,
                            Detail = this.Navigate<SportsViewModel>,
                            IconPath = "Sport.png"
                        }
                    }
                }
            };

            this.ToggleAuth();

            this.Items.Add(new NavSection
            {
                Title = AppResources.Settings,
                Items = new ObservableCollection<NavItem>
                {
                    new NavItem
                    {
                        Title = AppResources.Settings,
                        Detail = this.Navigate<SettingsViewModel>,
                        IconPath = "Setup.png"
                    }
                }
            });
        }

        public void Refresh()
        {
            this.ToggleAuth();
        }

        public void VerifyFirstRun(Action dieQuickly)
        {
            if (this.settings.GetValueOrDefault(FirstRunKey, true))
            {
                this.userInteraction.Dialog(
                    AppResources.AppFirstRunTitle,
                    AppResources.AppFirstRunMessage,
                    () => this.settings.AddOrUpdateValue(FirstRunKey, false),
                    dieQuickly);
            }
        }

        public void VerifyTimetableSync(Action handleTimetableSync)
        {
            if (this.settings.GetValueOrDefault(TtSyncDoneKey, false))
            {
                return;
            }

            handleTimetableSync?.Invoke();
            this.settings.AddOrUpdateValue(TtSyncDoneKey, true);
        }

        private void RemoveIfExists(string title)
        {
            var section = this.items.FirstOrDefault(t => t.Title == title);

            if (section != null)
            {
                this.items.Remove(section);
            }
        }

        private void ToggleAuth()
        {
            this.RemoveIfExists(AppResources.TileAccount);
            this.RemoveIfExists(AppResources.TileTimetable);

            if (this.IsAuth)
            {
                var navSection = new NavSection
                {
                    Title = AppResources.TileAccount,
                    Items = new ObservableCollection<NavItem>
                    {
                        new NavItem
                        {
                            Title = AppResources.TileBadge,
                            Detail = this.Navigate<AccountBalanceViewModel>,
                            IconPath = "Card.png"
                        },
                        new NavItem
                        {
                            Title = AppResources.TileLectureNotes,
                            Detail =
                                () =>
                                    this.Navigate<FilerViewModel, FilerViewModel.FilerArgs>(new FilerViewModel.FilerArgs
                                    {
                                        CurrentDirectory = string.Empty
                                    }),
                            IconPath = "Filer.png"
                        }
                    }
                };
                this.Items.Insert(1, navSection);

                var adunis = new NavSection
                {
                    Title = AppResources.TileTimetable,
                    Items = new ObservableCollection<NavItem>()
                };

                foreach (var period in this.adunisRepository.RetrieveNonEmptyTimeperiods())
                {
                    switch (period.Type)
                    {
                        case AdunisType.Timetable:
                            adunis.Items.Add(new NavItem
                            {
                                Title = period.Name,
                                Detail = () => this.Navigate<TimetableViewModel, object>(new { termId = period.TermID }),
                                IconPath = "Timetable.png"
                            });
                            break;
                        case AdunisType.Exam:
                            adunis.Items.Add(new NavItem
                            {
                                Title = period.Name,
                                Detail = () => this.Navigate<ExamViewModel, object>(new { termId = period.TermID }),
                                IconPath = "Exam.png"
                            });
                            break;
                        case AdunisType.Semester:
                        case AdunisType.ExamPreparation:
                            break;
                        default:
                            Debug.Assert(false, $"AdunisType: {period.Type}, not handled");
                            break;
                    }
                }

                adunis.Items.Add(new NavItem
                {
                    Title = AppResources.TileTimetable,
                    Detail = this.Navigate<AdunisViewModel>,
                    IconPath = "Timetable.png"
                });
                this.Items.Insert(2, adunis);
            }
            else
            {
                var navSection = new NavSection
                {
                    Title = AppResources.TileAccount,
                    Items = new ObservableCollection<NavItem>
                    {
                        new NavItem
                        {
                            Title = AppResources.TileSetupAccount,
                            Detail = this.Navigate<AccountViewModel>,
                            IconPath = "Setup.png"
                        }
                    }
                };
                this.Items.Insert(1, navSection);
            }
        }
    }
}
