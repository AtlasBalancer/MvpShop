using UnityEngine;
using com.ab.mvcshop.core.mvc;
using com.ab.mvcshop.core.command;
using com.ab.mvcshop.modules.health.model;
using com.ab.mvcshop.modules.health.signals;

namespace com.ab.mvcshop.modules.health.interacton
{
    
    [CreateAssetMenu (fileName = "$NAME$HealthChangeAmountCommandDef",menuName = "Commands/Health change amount")]
    public class HealthChangeAmountCommandSo : CommandSo
    {
        public int ValueToChange;

        public override IModel GetCost(CommandContext ctx) => 
            new Health(ValueToChange);

        public override void Execute(CommandContext ctx)
        {
            base.Execute(ctx);
            ctx.Signals.Fire(new HealthChangeAmountSignal(ValueToChange));
        }
    }
}