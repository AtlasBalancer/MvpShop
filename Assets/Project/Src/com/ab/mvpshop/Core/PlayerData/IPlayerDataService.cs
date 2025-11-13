using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.core.playerdata
{
    public interface IPlayerDataService
    {
        void Init<TModel>(bool rewrite = false) where TModel : IModel;
        TModel Get<TModel>() where TModel : IModel;
        void Commit<T>(T model);
    }
}