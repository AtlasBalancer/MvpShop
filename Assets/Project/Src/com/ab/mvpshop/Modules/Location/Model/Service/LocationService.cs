using System;
using System.Collections.Generic;
using System.Linq;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.localization;
using com.ab.mvpshop.core.mvp;
using com.ab.mvpshop.core.playerdata;
using Rx = R3;


namespace com.ab.mvpshop.modules.location
{
    public class LocationService : ILocationService
    {
        public IEnumerable<string> LocationLocalizedOptions { get; }
        public Rx.Observable<Location> ModelChanged => _model;

        readonly Settings _settings;
        readonly IPlayerDataService _persistent;
        readonly INotifyModelService _notifyModel;
        readonly ILocalizationService _localization;
        readonly Rx.BehaviorSubject<Location> _model;

        public LocationService(
            Settings settings,
            CommandInvoker commandInvoker,
            INotifyModelService notifyModel,
            IPlayerDataService persistent,
            ILocalizationService localization)
        {
            _settings = settings;
            _persistent = persistent;
            _notifyModel = notifyModel;
            _localization = localization;
            _persistent.Init<Location>();

            LocationLocalizedOptions = _settings.Locations
                .Select(item => _localization.GetString(item));
            
            Location persistRef = _persistent.Get<Location>();
            _model = new Rx.BehaviorSubject<Location>(persistRef);

            commandInvoker.Registry(typeof(Location), this);
        }

        public string Title => _localization.GetString(_model.Value.Title);

        public string TitleLk
        {
            get => _model.Value.Title;
            private set
            {
                if (value == _model.Value.Title) return;

                _model.Value.Title = value;
                _model.OnNext(_model.Value);
                _notifyModel.OnChange();
            }
        }

        public void ChangeAmount(string valueToChange) =>
            UpdateAmount(valueToChange);

        public void ChangeAmount(int indexLocation) => 
            ChangeAmount(_settings.Locations[indexLocation]);

        void UpdateAmount(string title)
        {
            Location location = new(title);
            _model.OnNext(location);
            _persistent.Commit(location);
        }

        public bool CanExecute(CommandContext ctx, IModel model)
        {
            var loation = (Location)model;
            var execute = _model.Value.Title == loation.Title;
            return execute;
        }

        [Serializable]
        public class Settings
        {
            public List<string> Locations;
        }
    }
}