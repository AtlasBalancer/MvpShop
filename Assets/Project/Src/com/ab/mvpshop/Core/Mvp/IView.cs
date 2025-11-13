using UnityEngine;

namespace com.ab.mvpshop.core.mvp
{
    public interface IView
    {
        void Show(Transform parent = null, bool saveWorld = false);

        void Hide();
    }
}