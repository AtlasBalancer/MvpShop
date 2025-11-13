using System;
using Zenject;
using UnityEngine;
using com.ab.mvpshop.modules.shop.model;

namespace com.ab.mvpshop.modules.shop.definition
{
    public class SingleBundleInstaller : MonoInstaller
    {
        public Settings _settings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SingleBundlePresenter>().AsSingle().NonLazy();
            Container.BindInstance(_settings.Controller);

            Container.Bind<IShopService>().To<ShopService>().AsSingle();
            Container.BindInstance(_settings.Service).AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public ShopService.Settings Service;
            public SingleBundlePresenter.Settings Controller;
        }
    }
}