using R3;
using System;
using Zenject;
using UnityEngine;
using System.Threading;
using com.ab.mvpshop.core.mvp;
using Cysharp.Threading.Tasks;
using com.ab.mvpshop.modules.vip.model;
using com.ab.mvpshop.modules.vip.interaction.Signals;

namespace com.ab.mvpshop.modules.vip.interaction
{
    public class VipPresenter : ViewPresenter<VipPanelView>
    {
        readonly Settings _settings;
        readonly IVipService _service;

        readonly TimeSpan _step;
        readonly DelayType _delayType;
        readonly CancellationTokenSource _cts;

        public VipPresenter(
            SignalBus signals,
            Settings settings,
            IVipService service,
            IViewFactory factory)
            : base(settings.PanelRoot, signals, settings.ViewAddressKey, factory)
        {
            _settings = settings;
            _service = service;
            _cts = new CancellationTokenSource();

            _step = _settings.StepMilliseconds > 0
                ? TimeSpan.FromMilliseconds(_settings.StepMilliseconds)
                : TimeSpan.FromMilliseconds(250);
            _delayType = _settings.UseUnscaled ? DelayType.UnscaledDeltaTime : DelayType.DeltaTime;

            UpdateRemainingTime();
        }

        async UniTaskVoid Timer(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                if (!_service.IsVipActive())
                {
                    await UniTask.Delay(_step);
                    continue;
                }

                UpdateRemainingTime();
                await UniTask.Delay(_step, _delayType, PlayerLoopTiming.FixedUpdate, ct);
            }
        }

        void UpdateRemainingTime() =>
            View.UpdateTime(_service.GetRemainingTime());

        public override void BindView(VipPanelView view)
        {
            base.UnBindView(view);
            Timer(_cts.Token).Forget();
            view.IncreaseAmount.onClick.AddListener(OnChangeExpirationTimeExternal);
        }

        public override void UnBindView(VipPanelView view)
        {
            base.UnBindView(view);
            _cts.Cancel();
        }

        public override void Subscribe(SignalBus signals) =>
            Signals.Subscribe<VipChangeSignal>(OnChangeExpirationTime);

        public override void Unsubscribe(SignalBus signals) =>
            Signals.Unsubscribe<VipChangeSignal>(OnChangeExpirationTime);

        public void OnChangeExpirationTimeExternal() =>
            OnChangeExpirationTime(new VipChangeSignal(
                TimeSpan.FromSeconds(_settings.IncreaseSecondsToChange)));

        public void OnChangeExpirationTime(VipChangeSignal signal)
        {
            var valueToChange = signal.ValueToChange;
            _service.ExtendExpirationTime(valueToChange);
        }

        public override void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            base.Dispose();
        }

        [Serializable]
        public class Settings
        {
            public bool UseUnscaled = true;
            public int StepMilliseconds = 2000;

            public int IncreaseSecondsToChange;
            public RectTransform PanelRoot;
            public string ViewAddressKey = VipAddressKey.VipPanel.ToString();
        }
    }
}