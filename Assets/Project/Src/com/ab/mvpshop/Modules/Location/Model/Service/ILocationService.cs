using System.Collections.Generic;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.playerdata;

namespace com.ab.mvpshop.modules.location
{
    public interface ILocationService : INotifyModelChanged<Location>, ICommandCanExecute
    {
        string Title { get; }
        public IEnumerable<string> LocationLocalizedOptions { get; }
        void ChangeAmount(string valueToChange);
        void ChangeAmount(int indexLocation);
    }
}