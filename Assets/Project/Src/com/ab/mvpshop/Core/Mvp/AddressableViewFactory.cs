using UnityEngine;
using System.ComponentModel;
using com.ab.mvpshop.core.assetlaod;

namespace com.ab.mvpshop.core.mvp
{
    public class AddressableViewFactory : IViewFactory
    {
        readonly IAddressableService _addressable;

        public AddressableViewFactory(IAddressableService addressable) => 
            _addressable = addressable;

        public TView Create<TView>(string id, Transform root = null) where TView : IView
        {
            GameObject prefab = _addressable.Load<GameObject>(id);

            if (prefab == null)
                throw new WarningException(
                    $"{nameof(AddressableViewFactory)}::{nameof(Create)}::{nameof(TView)}:Can't load {id}");
            
            return Object.Instantiate(prefab, root).GetComponent<TView>();
        }
    }
}