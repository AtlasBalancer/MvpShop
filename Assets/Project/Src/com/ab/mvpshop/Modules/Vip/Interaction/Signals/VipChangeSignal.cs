using System;

namespace com.ab.mvpshop.modules.vip.interaction.Signals
{
    public class VipChangeSignal
    {
        public TimeSpan ValueToChange;

        public VipChangeSignal(TimeSpan value) => 
            ValueToChange = value;
    }
}