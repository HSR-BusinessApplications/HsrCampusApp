// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using Plugin.Settings.Abstractions;
    using Resources;

    public class MapViewModel : AbstractParameterizedViewModel<object>, ITitled
    {
        private static readonly string FirstRunKey = typeof(MapViewModel).FullName + "FIRST_RUN";

        private readonly IUserInteractionService userInteraction;
        private readonly IDevice device;
        private readonly IMapRepository mapRepository;
        private readonly IMapSync mapSync;
        private readonly ISettings settings;
        private readonly IIOCacheService cache;

        private ObservableCollection<MapHashable> maps;
        private IEnumerable<MapHashable> sub;
        private MapHashable currentMap;
        private string title;

        public MapViewModel(IMvxNavigationService navigationService, ISettings settings, IUserInteractionService userInteraction, IDevice device, IMapRepository mapRepository, IMapSync mapSync, IIOCacheService cache)
        {
            this.NavigationService = navigationService;
            this.settings = settings;
            this.userInteraction = userInteraction;
            this.device = device;
            this.mapRepository = mapRepository;
            this.mapSync = mapSync;
            this.cache = cache;
        }

        public ICommand UpdateCommand => new MvxAsyncCommand(this.UpdateAsync);

        public MvxCommand<Point> LocationCommand => new MvxCommand<Point>(this.Click);

        public ObservableCollection<MapHashable> Maps
        {
            get
            {
                return this.maps;
            }

            set
            {
                this.maps = value;
                this.RaisePropertyChanged();
            }
        }

        public MapHashable CurrentMap
        {
            get
            {
                return this.currentMap;
            }

            set
            {
                this.currentMap = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a list of all building maps which belong to <see cref="CurrentMap"/>
        /// The value is null except for the root map
        /// </summary>
        public IEnumerable<MapHashable> Sub
        {
            get
            {
                return this.sub;
            }

            set
            {
                this.sub = value;
                this.RaisePropertyChanged();
            }
        }

        public Guid? Id { get; set; }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.RaisePropertyChanged();
            }
        }

        public void Init(string id, string title) // cannot use Guid as parameter due to mvvmcross limitations
        {
            if (!string.IsNullOrEmpty(id))
            {
                this.Id = Guid.Parse(id);
            }

            this.Title = title;
        }

        public override void Prepare(object parameter)
        {
        }

        public override void Start()
        {
            base.Start();

            this.LoadCache(this.Id);

            if (!this.Id.HasValue)
            { // only update on root
                Task.Run(this.UpdateAsync);
            }

            if (!this.settings.GetValueOrDefault(FirstRunKey, true))
            {
                return;
            }

            var message = AppResources.MapFirstRunMessage;

            if (this.device.Platform == DevicePlatform.iOS)
            {
                message = AppResources.MapFirstRunMessageiOS;
            }

            this.userInteraction.Dialog(
                AppResources.MapFirstRunTitle,
                message,
                () => this.settings.AddOrUpdateValue(FirstRunKey, false));
        }

        public void LoadCache(Guid? id)
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            if (!id.HasValue)
            {
                var root = this.mapRepository.RetrieveRoot();

                if (root != null && this.cache.FileExists(root.ImagePath))
                {
                    this.Maps = new ObservableCollection<MapHashable> { root };

                    this.Sub = this.mapRepository.RetrieveEntries(root.Id);
                }
            }
            else
            {
                this.Maps = new ObservableCollection<MapHashable>(this.mapRepository.RetrieveEntries(id.Value));
            }

            if (this.Maps == null)
            {
                this.HasContent = false;
                this.IsLoading = false;
                return;
            }

            this.HasContent = this.Maps.Count > 0;

            if (this.HasContent)
            {
                this.CurrentMap = this.Maps[0];
            }

            this.IsLoading = false;
        }

        public async Task UpdateAsync()
        {
            if (this.IsUpdating)
            {
                return;
            }

            this.IsUpdating = true;
            var res = await this.mapSync.UpdateAsync(this.ObtainCancellationToken());

            if (res == ResultState.Success)
            {
                this.LoadCache(this.Id);
            }

            this.IsUpdating = false;
        }

        public void GoDeeper(Guid map, string title)
        {
            this.Navigate<MapViewModel, object>(new { id = map, title });
        }

        public void Click(Point point)
        {
            if (this.Sub == null)
            {
                return;
            }

            foreach (var map in this.Sub)
            {
                var dAr = map.Coordinates.Split(',');
                var rect = new Rect(double.Parse(dAr[0]), double.Parse(dAr[1]), double.Parse(dAr[2]), double.Parse(dAr[3]));

                if (!rect.Contains(point.X, point.Y))
                {
                    continue;
                }

                this.GoDeeper(map.Id, map.Name);
                break;
            }
        }

        public struct Point
        {
            public Point(double x, double y)
                : this()
            {
                this.X = x;
                this.Y = y;
            }

            public double X { get; }

            public double Y { get; }
        }

        private struct Rect
        {
            public Rect(double topLeftX, double topLeftY, double bottomRightX, double bottomRightY)
            {
                this.TopLeft = new Point(topLeftX, topLeftY);
                this.BottomRight = new Point(bottomRightX, bottomRightY);
            }

            private Point TopLeft { get; }

            private Point BottomRight { get; }

            public bool Contains(double x, double y) => this.TopLeft.X <= x && x < this.BottomRight.X && this.TopLeft.Y <= y && y < this.BottomRight.Y;
        }
    }
}
