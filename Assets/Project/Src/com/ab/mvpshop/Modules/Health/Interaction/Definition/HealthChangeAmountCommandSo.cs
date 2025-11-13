using UnityEngine;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.mvp;
using com.ab.mvpshop.modules.health.model;
using com.ab.mvpshop.modules.health.signals;

namespace com.ab.mvpshop.modules.health.interacton
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