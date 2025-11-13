using System;
using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.modules.gold.model
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