using com.ab.mvcshop.core.command;
using com.ab.mvcshop.core.mvc;
using com.ab.mvcshop.core.playerdata;
using Rx = R3;

namespace com.ab.mvcshop.modules.health.model
{
    public class HealthService : IHealthService
    {
        readonly IPlayerDataService _persistent;
        readonly INotifyModelService _notifyModel;

        readonly Rx.BehaviorSubject<Health> _model;
        public Rx.Observable<Health> ModelChanged => _model;

        public HealthService(
            CommandInvoker commandInvoker,
            INotifyModelService notifyModel,
            IPlayerDataService persistent)
        {
            _notifyModel = notifyModel;
            _persistent = persistent;
            _persistent.Init<Health>();
            Health persistRef = _persistent.Get<Health>();
            _model = new Rx.BehaviorSubject<Health>(persistRef);

            commandInvoker.Registry(typeof(Health), this);
        }
        
        public int Amount
        {
            get => _model.Value.Amount;
            private set
            {
                if (value == _model.Value.Amount) return;

                _model.Value.Amount = value;
                _model.OnNext(_model.Value);
                _notifyModel.OnChange();
            }
        }

        public void ChangeAmount(int valueToChange) =>
            UpdateAmount(Amount + valueToChange);

        void UpdateAmount(int amount)
        {
            Amount = amount;
            _persistent.Commit(_model.Value);
        }

        public bool CanExecute(CommandContext ctx, IModel model)
        {
            var gold = (Health)model;
            var execute = (_model.Value.Amount + gold.Amount) >= 0;
            return execute;
        }
    }
}