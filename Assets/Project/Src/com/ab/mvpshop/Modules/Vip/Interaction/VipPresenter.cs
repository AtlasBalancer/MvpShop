using R3;
using System;
using Zenject;
using UnityEngine;
using com.ab.mvpshop.core.mvp;
using com.ab.mvpshop.modules.vip.interaction.Signals;
using com.ab.mvpshop.modules.vip.model;

namespace com.ab.mvpshop.modules.vip.interaction
{
    public class VipPresenter : ViewPresenter<VipPanelView>
    {
        readonly Settings _settings;
        readonly IVipService _service;

        public VipPresenter(
            SignalBus signals,
            Settings settings,
            IVipService service,
            IViewFactory factory)
            : base(settings.PanelRoot, signals, settings.ViewAddressKey, factory)
        {
            _settings = settings;
            _service = service;
        }

        public override void BindView(VipPanelView view)
        {
            view.IncreaseAmount.onClick.AddListener(OnChangeAmountExternal);

            _service.ModelChanged
                .Select(item => item.Amount)
                .DistinctUntilChanged()
                .Subscribe(view.UpdateTime)
                .AddTo(Disposables);
        }

        public void OnChangeAmountExternal() =>
            OnChangeAmount(new VipChangeSignal(
                TimeSpan.FromSeconds(_settings.IncreaseSecondsToChange)));

        public override void Subscribe(SignalBus signals) =>
            Signals.Subscribe<VipChangeSignal>(OnChangeAmount);

        public override void Unsubscribe(SignalBus signals) =>
            Signals.Unsubscribe<VipChangeSignal>(OnChangeAmount);

        public void OnChangeAmount(VipChangeSignal signal)
        {
            var valueToChange = signal.ValueToChange;
            _service.ChangeAmount(valueToChange);
        }

        [Serializable]
        public class Settings
        {
            public int IncreaseSecondsToChange;
            public RectTransform PanelRoot;
            public string ViewAddressKey = VipAddressKey.VipPanel.ToString();
        }
    }
}