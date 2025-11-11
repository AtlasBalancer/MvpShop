using System;
using com.ab.mvcshop.core.playerdata;
using com.ab.mvcshop.modules.health.model;
using com.ab.mvcshop.modules.health.signals;
using Zenject;

namespace com.ab.mvcshop.modules.health.interacton
{
    public class HealthInstaller : MonoInstaller
    {
        public Settings _settings;

        public override void InstallBindings()
        {
            DevProfile();

            Container.BindInterfacesAndSelfTo<HealthController>().AsSingle().NonLazy();
            Container.BindInstance(_settings.Controller).AsSingle();
            Container.Bind<IHealthService>().To<HealthService>().AsSingle();
            Container.DeclareSignal<HealthChangeAmountSignal>();
        }

        void DevProfile()
        {
            if (!_settings.Default.OverrideModel)
                return;

            var dataService = Container.Resolve<MockPlayerDataService>();
            dataService.Registry<Health>(_settings.Default.Model);
        }

        [Serializable]
        public class Settings
        {
            public MockData Default;
            public HealthController.Settings Controller;
        }

        [Serializable]
        public class MockData
        {
            public Health Model;
            public bool OverrideModel;
        }
    }
}