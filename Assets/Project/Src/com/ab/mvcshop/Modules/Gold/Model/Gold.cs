using System;
using com.ab.mvcshop.core.mvc;

namespace com.ab.mvcshop.modules.gold.model
{
    [Serializable]
    public class Gold : AmountModel<int>
    {
        public Gold() : base(0) { }

        public Gold(int amount) : base(amount) { }

        public override void Combine(IModel model)
        {
            Gold combine = model as Gold;
            Amount += combine.Amount;
        }
    }
}