using R3;
using System;
using Zenject;
using UnityEngine;
using com.ab.mvcshop.core.mvc;
using com.ab.mvcshop.modules.health.model;
using com.ab.mvcshop.modules.health.signals;

namespace com.ab.mvcshop.modules.health.interacton
{
    public class HealthController : ViewController<HealthView>
    {
        readonly Settings _settings;
        readonly IHealthService _service;

        public HealthController(
            SignalBus signals,
            Settings settings,
            IHealthService service,
            IViewFactory factory)
            : base(settings.PanelRoot, signals, settings.ViewAddressKey, factory)
        {
            _service = service;
            _settings = settings;
        }

        public override void BindView(HealthView view)
        {
            view.IncreaseAmount.onClick.AddListener(OnChangeAmountExternal);

            _service.ModelChanged
                .Select(item => item.Amount)
                .DistinctUntilChanged()
                .Subscribe(view.UpdateAmount)
                .AddTo(Disposables);
        }

        public override void UnBindView(HealthView view)
        {
            view.IncreaseAmount.onClick.RemoveListener(OnChangeAmountExternal);
            Disposables.Clear();
        }

        public override void Subscribe(SignalBus signals) =>
            Signals.Subscribe<HealthChangeAmountSignal>(OnChangeAmount);

        public override void Unsubscribe(SignalBus signals) =>
            Signals.Unsubscribe<HealthChangeAmountSignal>(OnChangeAmount);

        public void OnChangeAmountExternal() =>
            OnChangeAmount(new HealthChangeAmountSignal(
                _settings.IncreaseAmountValueToChange));

        public void OnChangeAmount(HealthChangeAmountSignal signal)
        {
            var valueToChange = signal.ValueToChange;
            _service.ChangeAmount(valueToChange);
        }

        [Serializable]
        public class Settings
        {
            public int IncreaseAmountValueToChange;
            public RectTransform PanelRoot;
            public string ViewAddressKey = HealthAddressKey.HealthAmountPanel.ToString();
        }
    }
}