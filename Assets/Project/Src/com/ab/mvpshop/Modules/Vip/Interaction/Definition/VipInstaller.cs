using System;
using Zenject;
using com.ab.mvpshop.core.playerdata;
using com.ab.mvpshop.modules.vip.interaction.Signals;
using com.ab.mvpshop.modules.vip.model;

namespace com.ab.mvpshop.modules.vip.interaction
{
    public class VipInstaller : MonoInstaller
    {
        public Settings _settings;

        public override void InstallBindings()
        {
            DevProfile();

            Container.BindInterfacesAndSelfTo<VipPresenter>().AsSingle().NonLazy();
            Container.BindInstance(_settings.Controller).AsSingle();
            Container.BindInterfacesAndSelfTo<VipService>().AsSingle();
            Container.BindInstance(_settings.Service).AsSingle();
            Container.DeclareSignal<VipChangeSignal>();
        }

        void DevProfile()
        {
            if (!_settings.Default.OverrideModel)
                return;

            var dataService = Container.Resolve<MockPlayerDataService>();
            dataService.Registry<Vip>(new Vip(TimeSpan.FromSeconds(_settings.Default.SecondsVip)));
        }

        [Serializable]
        public class Settings
        {
            public MockData Default;
            public VipService.Settings Service;
            public VipPresenter.Settings Controller;
        }

        [Serializable]
        public class MockData
        {
            public float SecondsVip;
            public bool OverrideModel;
        }
    }
}