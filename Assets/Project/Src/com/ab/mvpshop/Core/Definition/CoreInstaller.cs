using System;
using Zenject;
using UnityEngine;
using com.ab.mvpshop.core.assetlaod;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.localization;
using com.ab.mvpshop.core.mvp;
using com.ab.mvpshop.core.playerdata;

namespace com.ab.mvpshop.core.definition
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] Settings _settings;

        public override void InstallBindings()
        {
            DevProfile();

            SignalBusInstaller.Install(Container);
            
            Container.BindInstance(_settings).WithId(Settings.UI_ROOT_DI_KEY).AsSingle();
            Container.Bind<IAddressableService>().To<AddressableService>().AsSingle();
            Container.Bind<IViewFactory>().To<AddressableViewFactory>().AsSingle();
            Container.Bind<ILocalizationService>().To<LocalizationService>().AsSingle();
            Container.Bind<ICommandService>().To<CommandService>().AsSingle();
            Container.Bind<CommandInvoker>().To<CommandInvoker>().AsSingle();
            Container.Bind<INotifyModelService>().To<NotifyModelService>().AsSingle();
            Container.Bind<CommandContext>().AsSingle();
        }

        void DevProfile()
        {
            Container.BindInterfacesAndSelfTo<MockPlayerDataService>().AsSingle();
            Container.BindInstance(_settings.Mock);
        }

        [Serializable]
        public class Settings
        {
            public Canvas UiRoot;
            public const string UI_ROOT_DI_KEY = "UiRoot";

            public MockPlayerDataService.Settings Mock;
        }
    }
}