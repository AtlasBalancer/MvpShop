using TMPro;
using System;
using com.ab.mvcshop.core.mvc;

namespace com.ab.mvcshop.modules.vip.interaction
{
    public class VipPanelView : AmountView
    {
        public TMP_Text Time;

        public void UpdateTime(TimeSpan time) => 
            Time.text = time.ToString(@"mm\:ss");
    }
}