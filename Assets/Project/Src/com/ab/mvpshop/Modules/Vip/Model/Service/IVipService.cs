using System;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.playerdata;

namespace com.ab.mvpshop.modules.vip.model
{
    public interface IVipService: INotifyModelChanged<Vip>, ICommandCanExecute, IDisposable
    {
        void ChangeAmount(TimeSpan valueToChange);
    }
}