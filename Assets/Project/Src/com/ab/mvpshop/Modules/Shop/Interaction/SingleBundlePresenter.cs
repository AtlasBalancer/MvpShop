using R3;
using System;
using Zenject;
using UnityEngine;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.mvp;
using com.ab.mvpshop.core.playerdata;
using com.ab.mvpshop.modules.shop.model;
using UnityEngine.UI;

namespace com.ab.mvpshop.modules.shop.definition
{
    public class SingleBundlePresenter : ViewPresenter<BundleView>
    {
        readonly Settings _settings;
        readonly ShopService Service;
        readonly IShopService _service;
        readonly IViewFactory _factory;
        readonly ShopScenePayLoad _payLoad;
        readonly SceneLoaderService _sceneLoader;
        readonly CommandInvoker _commandInvoker;
        readonly BundlesPresenter _bundlesPresenter;

        public SingleBundlePresenter(
            SignalBus signals,
            Settings settings,
            IViewFactory factory,
            IShopService service,
            ShopScenePayLoad payLoad,
            SceneLoaderService sceneLoader,
            CommandInvoker commandInvoker)
            : base(settings.FullBundleRoot, signals, settings.FullBundleAddressKey, factory)
        {
            _payLoad = payLoad;
            _service = service;
            _factory = factory;
            _settings = settings;
            _bundlesPresenter = new();
            _sceneLoader = sceneLoader;
            _commandInvoker = commandInvoker;
            
            _settings.GoToShop.onClick.AddListener(OnGoToShop);
        }

        void OnGoToShop() => 
            _sceneLoader.LoadAsync(_settings.ShopScene);

        void CreateBundle()
        {
            Bundle bundle = _service.GetBundle(_payLoad.BundleID);
            View.SetUp(bundle);
            _bundlesPresenter.Add(bundle, View);
        }

        public override void Show(BundleView view)
        {
            CreateBundle();
            OnCheckBundleAvailabilityCondition();
            base.Show(view);

            OnCheckBundleAvailabilityCondition();
            _service.ModelChanged.Subscribe(_ =>
                    OnCheckBundleAvailabilityCondition())
                .AddTo(Disposables);
        }

        public void OnCheckBundleAvailabilityCondition()
        {
            foreach (var item in _bundlesPresenter.Refs)
            {
                CommandCostComposite cost = item.Value.Bundle.Conditions;

                var canExecute = _commandInvoker.CanExecute(cost);
                item.Value.View.BuyAvailable(canExecute);
            }
        }

        public override void BindView(BundleView view)
        {
            _bundlesPresenter.BuyClick.Subscribe(OnBuyClick)
                .AddTo(Disposables);
        }

        public void OnBuyClick(int bundleId)
        {
            var bundle = _bundlesPresenter.GetBundle(bundleId);
            _service.BuyBundle(bundle);
        }

        [Serializable]
        public class Settings
        {
            public RectTransform FullBundleRoot;
            public string FullBundleAddressKey = ShopAddressKey.FullBundleView.ToString();
            public Button GoToShop;
            public string ShopScene = "Shop";
        }
    }
}