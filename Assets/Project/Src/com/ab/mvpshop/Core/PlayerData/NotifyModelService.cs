using R3;

namespace com.ab.mvpshop.core.playerdata
{
    public class NotifyModelService : INotifyModelService
    {
        readonly Subject<Unit> _changed = new();
        public Observable<Unit> Changed => _changed;

        public void OnChange() => 
            _changed.OnNext(Unit.Default);

        public void Dispose() => _changed.Dispose();
    }
}