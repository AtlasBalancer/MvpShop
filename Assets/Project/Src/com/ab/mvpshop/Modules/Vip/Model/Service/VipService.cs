using System;
using Rx = R3;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.mvp;
using com.ab.mvpshop.core.playerdata;

namespace com.ab.mvpshop.modules.vip.model
{
    public class VipService : IVipService
    {
        readonly Settings _settings;
        readonly IPlayerDataService _persistent;
        readonly INotifyModelService _notifyModel;

        readonly Rx.BehaviorSubject<Vip> _model;
        public Rx.Observable<Vip> ModelChanged => _model;

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

        }

        public DateTimeOffset ExpiredTime
        {
            get => _model.Value.ExpirationTime;
            private set
            {
                if (value == _model.Value.ExpirationTime)
                    return;

                _model.Value.ExpirationTime = value;
                _model.OnNext(_model.Value);
                _notifyModel.OnChange();
            }
        }

        public void ExtendExpirationTime(TimeSpan valueToChange)
        {
            var nowUtc = DateTimeOffset.UtcNow;
            var expirationTime = _model.Value.ExpirationTime;

            if (IsVipActive())
                UpdateExpiredTime(expirationTime + valueToChange);
            else
                UpdateExpiredTime(nowUtc + valueToChange);
        }

        public TimeSpan GetRemainingTime()
        {
            if(!IsVipActive())
                return TimeSpan.Zero;

            return DateTimeOffset.UtcNow - _model.Value.ExpirationTime;
        }

        public bool IsVipActive() =>
            ExpiredTime.Ticks > 0 && ExpiredTime > DateTimeOffset.UtcNow;

        void UpdateExpiredTime(DateTimeOffset newTime)
        {
            ExpiredTime = newTime;
            _persistent.Commit(_model);
        }

        public bool CanExecute(CommandContext ctx, IModel model) =>
            _model.Value.Amount > TimeSpan.Zero;


        [Serializable]
        public class Settings { }
    }
}