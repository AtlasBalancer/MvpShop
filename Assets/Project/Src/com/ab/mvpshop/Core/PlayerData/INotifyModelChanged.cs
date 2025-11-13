using R3;

namespace com.ab.mvpshop.core.playerdata
{
    public interface INotifyModelChanged<TModel> 
    {
        Observable<TModel> ModelChanged { get; }
    }
}