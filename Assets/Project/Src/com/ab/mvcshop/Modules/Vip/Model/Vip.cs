using System;
using com.ab.mvcshop.core.mvc;

namespace com.ab.mvcshop.modules.vip.model
{
    [Serializable]
    public class Vip : AmountModel<TimeSpan>
    {
        public Vip() : base(TimeSpan.Zero) { }

        public TimeSpan Amount;

        public Vip(TimeSpan amount) : base(amount) =>
            Amount = amount;

        public override void Combine(IModel model) =>
            Amount += ((Vip)model).Amount;
    }
}