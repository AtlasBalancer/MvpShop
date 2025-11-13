using UnityEngine;

namespace com.ab.mvpshop.core.mvp
{
    public interface IViewFactory
    {
        public TView Create<TView>(string id, Transform root = null) where TView : IView;
    }
}