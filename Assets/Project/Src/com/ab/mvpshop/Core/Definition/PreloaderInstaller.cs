using System;
using Zenject;
using com.ab.mvpshop.core.playerdata;

namespace com.ab.mvpshop.core.definition
{
    public class PreloaderInstaller : MonoInstaller
    {
        public Settings _settings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneLoaderService>().AsSingle().NonLazy();
            Container.BindInstance(_settings.SceneLoader).AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public SceneLoaderService.Settings SceneLoader;
        }
    }
}