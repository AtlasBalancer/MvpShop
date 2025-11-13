using System;
using Zenject;
using UnityEngine;
using com.ab.mvpshop.core.playerdata;

namespace com.ab.mvpshop.modules.shop.definition
{
    public class ShopPayloadInstaller : MonoInstaller
    {
        public Settings _settings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_settings.PayLoad);
        }

        [Serializable]
        public class Settings
        {
            public Transform PayLoadContainer;
            public ShopScenePayLoad PayLoad;
        }
    }
}