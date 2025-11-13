using R3;
using System.Collections.Generic;
using Bundle = com.ab.mvpshop.modules.shop.model.Bundle;

namespace com.ab.mvpshop.modules.shop
{
    using Bundle = model.Bundle;

    public class BundlesPresenter
    {
        public readonly Dictionary<int, BundleEntry> Refs;

        public readonly Subject<int> BuyClick;
        public readonly Subject<int> InfoClick;

        public BundlesPresenter()
        {
            Refs = new();
            BuyClick = new();
            InfoClick = new();
        }

        public Bundle GetBundle(int bundleId)
        {
            return Refs[bundleId].Bundle;
        }

        public void Add(Bundle bundle, BundleView view)
        {
            view.Info.onClick.AddListener(() => InfoClick.OnNext(bundle.Id));
            view.Buy.onClick.AddListener(() => BuyClick.OnNext(bundle.Id));
            Refs.Add(bundle.Id, new BundleEntry(bundle, view));
        }

        public class BundleEntry
        {
            public readonly Bundle Bundle;
            public readonly BundleView View;

            public BundleEntry(Bundle bundle, BundleView view)
            {
                Bundle = bundle;
                View = view;
            }
        }
    }
}