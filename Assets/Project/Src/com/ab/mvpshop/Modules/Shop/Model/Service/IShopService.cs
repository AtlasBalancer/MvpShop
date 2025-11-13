using R3;
using System.Collections.Generic;

namespace com.ab.mvpshop.modules.shop.model
{
    public interface IShopService 
    {
        Bundle GetBundle(int bundleID);
        List<Bundle> GetBundles();
        Observable<Unit> ModelChanged { get; }
        void BuyBundle(Bundle bundle);
    }
}