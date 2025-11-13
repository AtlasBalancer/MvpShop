using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.playerdata;

namespace com.ab.mvpshop.modules.health.model
{
    public interface IHealthService : INotifyModelChanged<Health>, ICommandCanExecute
    {
        int Amount { get; }
        void ChangeAmount(int valueToChange);
    }
}