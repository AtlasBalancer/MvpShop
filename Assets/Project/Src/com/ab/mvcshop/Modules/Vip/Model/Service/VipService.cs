using System;
using Rx = R3;
using System.Threading;
using Cysharp.Threading.Tasks;
using com.ab.mvcshop.core.mvc;
using com.ab.mvcshop.core.command;
using com.ab.mvcshop.core.playerdata;

namespace com.ab.mvcshop.modules.vip.model
{
    public class VipService : IVipService
    {
        readonly Settings _settings;
        readonly IPlayerDataService _persistent;
        readonly INotifyModelService _notifyModel;

        readonly Rx.BehaviorSubject<Vip> _model;
        public Rx.Observable<Vip> ModelChanged => _model;

        readonly TimeSpan _step;
        readonly DelayType _delayType;

        CancellationTokenSource _cts;

        public VipService(
            Settings settings,
            CommandInvoker commandInvoker,
            INotifyModelService notifyModel,
            IPlayerDataService persistent)
        {
            _settings = settings;
            _notifyModel = notifyModel;
            _persistent = persistent;
            _persistent.Init<Vip>();
            Vip persistRef = _persistent.Get<Vip>();
            _model = new Rx.BehaviorSubject<Vip>(persistRef);

            commandInvoker.Registry(typeof(Vip), this);

            _step = _settings.StepMilliseconds > 0
                ? TimeSpan.FromMilliseconds(250)
                : TimeSpan.FromMilliseconds(_settings.StepMilliseconds);
            _delayType = _settings.UseUnscaled ? DelayType.UnscaledDeltaTime : DelayType.DeltaTime;

            Start();
        }

        public void Start()
        {
            Stop();
            _cts = new CancellationTokenSource();
            Timer(_cts.Token).Forget();
        }

        public void Stop()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        async UniTaskVoid Timer(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                if (Amount <= TimeSpan.Zero)
                {
                    await UniTask.Delay(_step);
                    continue;
                }

                await UniTask.Delay(_step, _delayType, PlayerLoopTiming.FixedUpdate, ct);
                var left = Amount - _step;

                Amount = left > TimeSpan.Zero ? left : TimeSpan.Zero;
            }
        }

        public TimeSpan Amount
        {
            get => _model.Value.Amount;
            private set
            {
                if (value == _model.Value.Amount) 
                    return;

                _model.Value.Amount = value;
                _model.OnNext(_model.Value);
                _notifyModel.OnChange();
            }
        }

        public void ChangeAmount(TimeSpan valueToChange)
        {
            _model.Value.Amount += valueToChange;
            UpdateAmount(_model.Value.Amount);
        }

        void UpdateAmount(TimeSpan amount)
        {
            Amount = amount;
            _persistent.Commit(_model);
        }

        public bool CanExecute(CommandContext ctx, IModel model) =>
            _model.Value.Amount > TimeSpan.Zero;


        [Serializable]
        public class Settings
        {
            public bool UseUnscaled = true;
            public int StepMilliseconds = 2000;
        }

        public void Dispose() => Stop();
    }
}