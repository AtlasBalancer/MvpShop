using System;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.playerdata;

namespace com.ab.mvpshop.modules.vip.model
{
    public interface IVipService: INotifyModelChanged<Vip>, ICommandCanExecute
    {
        bool IsVipActive();
        
        void ExtendExpirationTime(TimeSpan valueToChange);

        TimeSpan GetRemainingTime();
    }
}