using System;
using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.modules.health.model
{
    [Serializable]
    public class Health : AmountModel<int>
    {
        public Health() : base(0) { }
        
        public Health(int amount) 
            : base(amount) { }

        public override void Combine(IModel model) => 
            Amount += ((Health)model).Amount;
    }
}