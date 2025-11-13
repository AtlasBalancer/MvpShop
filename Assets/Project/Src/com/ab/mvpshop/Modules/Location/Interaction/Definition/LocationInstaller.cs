using System;
using com.ab.mvpshop.core.playerdata;
using Zenject;

namespace com.ab.mvpshop.modules.location
{
   public class LocationInstaller : MonoInstaller
    {
        public Settings _settings;

        public override void InstallBindings()
        {
            DevProfile();

            Container.BindInterfacesAndSelfTo<LocationPresenter>().AsSingle().NonLazy();
            Container.BindInstance(_settings.Controller).AsSingle();
            Container.DeclareSignal<LocationChangeSignal>();
            
            Container.Bind<ILocationService>().To<LocationService>().AsSingle();
            Container.BindInstance(_settings.Service);
        }

        void DevProfile()
        {
            if (!_settings.Default.OverrideModel)
                return;

            var dataService = Container.Resolve<MockPlayerDataService>();
            dataService.Registry<Location>(_settings.Default.Model);
        }

        [Serializable]
        public class Settings
        {
            public MockData Default;
            public LocationService.Settings Service;
            public LocationPresenter.Settings Controller;
            
            // location.data.forest 
            // location.data.dungeon
            // location.data.town
            // location.data.toxicruins
        }

        [Serializable]
        public class MockData
        {
            public Location Model;
            public bool OverrideModel;
        }
    }
}