using UnityEngine;

namespace com.ab.mvpshop.core.playerdata
{
    public class PreloaderView : MonoBehaviour
    {
        public void Active(bool active) =>
            gameObject.SetActive(active);
    }
}