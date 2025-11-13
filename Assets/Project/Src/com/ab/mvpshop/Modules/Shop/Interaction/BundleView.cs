using System;
using TMPro;
using com.ab.mvpshop.core.mvp;
using com.ab.mvpshop.modules.shop.model;
using UnityEngine.UI;

namespace com.ab.mvpshop.modules.shop
{
    public class BundleView : BaseView
    {
        public Button Buy;
        public Button Info;
        public TMP_Text Description;
        public TMP_Text NotEnoughResource;

        public void SetUp(Bundle item)
        {
            Description.SetText(item.Message);
            Description.ForceMeshUpdate();   
        }

        public void BuyAvailable(bool canExecute)
        {
            Buy.interactable = canExecute;
            Buy.gameObject.SetActive(canExecute);
            NotEnoughResource.gameObject.SetActive(!canExecute);
        }
    }
}