using R3;
using System;
using Zenject;
using UnityEngine;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.mvp;
using com.ab.mvpshop.core.playerdata;
using com.ab.mvpshop.modules.shop.definition;
using com.ab.mvpshop.modules.shop.interaction;
using com.ab.mvpshop.modules.shop.model;

namespace com.ab.mvpshop.modules.shop
{
    public class ShopPresenter : ViewPresenter<ShopView>
    {
        readonly Settings _settings;
        readonly ShopService Service;
        readonly IShopService _service;
        readonly IViewFactory _factory;
        readonly ShopScenePayLoad _payLoad;
        readonly CommandInvoker _commandInvoker;
        readonly SceneLoaderService _sceneLoader;
        readonly BundlesPresenter _bundlesPresenter;

        public ShopPresenter(
            SignalBus signals,
            Settings settings,
            IViewFactory factory,
            IShopService service,
            ShopScenePayLoad payLoad,
            SceneLoaderService sceneLoad,
            CommandInvoker commandInvoker)
            : base(settings.ShopRoot, signals, settings.ShopViewAddressKey, factory)
        {
            _service = service;
            _factory = factory;
            _payLoad = payLoad;
            _settings = settings;
            _sceneLoader = sceneLoad;
            _bundlesPresenter = new();
            _commandInvoker = commandInvoker;
        }

        public override void Show(ShopView view)
        {
            base.Show(view);
            CreateBundles();
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

        public override void BindView(ShopView view)
        {
            _bundlesPresenter.InfoClick.Subscribe(OnInfoClick)
                .AddTo(Disposables);

            _bundlesPresenter.BuyClick.Subscribe(OnBuyClick)
                .AddTo(Disposables);
        }

        public void OnInfoClick(int bundleId)
        {
            _payLoad.UpdateBundleId(bundleId);
            _sceneLoader.LoadAsync(_settings.InfoSceneName);
        }

        public void OnBuyClick(int bundleId)
        {
            var bundle = _bundlesPresenter.GetBundle(bundleId);
            _service.BuyBundle(bundle);
        }

        void CreateBundles()
        {
            foreach (var item in _service.GetBundles())
            {
                var bundle = _factory.Create<BundleView>(
                    _settings.BundleViewAddressKey, 
                    View.BundlesRoot);
                
                bundle.SetUp(item);
                _bundlesPresenter.Add(item, bundle);
            }
        }

        [Serializable]
        public class Settings
        {
            public RectTransform ShopRoot;
            public string InfoSceneName;

            public string ShopViewAddressKey = ShopAddressKey.ShopView.ToString();
            public string BundleViewAddressKey = ShopAddressKey.BundleView.ToString();
        }
    }
}