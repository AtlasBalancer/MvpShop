using TMPro;
using System;
using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.modules.vip.interaction
{
    public class VipPanelView : AmountView
    {
        public TMP_Text Time;

        public void UpdateTime(TimeSpan time) => 
            Time.text = time.ToString(@"mm\:ss");
    }
}