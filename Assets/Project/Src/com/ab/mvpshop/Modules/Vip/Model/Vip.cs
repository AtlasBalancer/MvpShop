using System;
using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.modules.vip.model
{
    [Serializable]
    public class Vip : AmountModel<TimeSpan>
    {
        public Vip() : base(TimeSpan.Zero) { }

        public TimeSpan Amount;

        public DateTimeOffset ExpirationTime;
        
        public Vip(TimeSpan amount) : base(amount) =>
            Amount = amount;

        public override void Combine(IModel model) =>
            Amount += ((Vip)model).Amount;
    }
}