using System;
using Zenject;
using UnityEngine;
using com.ab.mvpshop.core.playerdata;
using com.ab.mvpshop.modules.gold.interaction;
using com.ab.mvpshop.modules.gold.model;
using com.ab.mvpshop.modules.gold.signals;

namespace com.ab.mvpshop.modules.gold.definition
{
    public class GoldInstaller : MonoInstaller
    {
        [SerializeField] Settings _settings;

        public override void InstallBindings()
        {
            DevProfile();

            Container.BindInterfacesAndSelfTo<GoldPresenter>().AsSingle().NonLazy();
            Container.BindInstance(_settings.Controller).AsSingle();
            Container.Bind<IGoldService>().To<GoldService>().AsSingle();
            Container.DeclareSignal<GoldChangeAmountSignal>();
        }

        void DevProfile()
        {
            if (!_settings.Default.OverrideModel)
                return;

            var dataService = Container.Resolve<MockPlayerDataService>();
            dataService.Registry<Gold>(_settings.Default.Model);
        }

        [Serializable]
        public class Settings
        {
            public MockData Default;
            public GoldPresenter.Settings Controller;
        }

        [Serializable]
        public class MockData
        {
            public Gold Model;
            public bool OverrideModel;
        }
    }
}