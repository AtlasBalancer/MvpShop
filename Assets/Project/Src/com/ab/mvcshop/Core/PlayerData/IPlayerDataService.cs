using com.ab.mvcshop.core.mvc;

namespace com.ab.mvcshop.core.playerdata
{
    public interface IPlayerDataService
    {
        void Init<TModel>(bool rewrite = false) where TModel : IModel;
        TModel Get<TModel>() where TModel : IModel;
        void Commit<T>(T model);
    }
}