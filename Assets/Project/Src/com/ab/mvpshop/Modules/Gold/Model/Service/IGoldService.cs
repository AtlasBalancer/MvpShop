using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.playerdata;

namespace com.ab.mvpshop.modules.gold.model
{
    public interface IGoldService : INotifyModelChanged<Gold>, ICommandCanExecute
    {
        int Amount { get; }
        void ChangeAmount(int valueToChange);
    }
}