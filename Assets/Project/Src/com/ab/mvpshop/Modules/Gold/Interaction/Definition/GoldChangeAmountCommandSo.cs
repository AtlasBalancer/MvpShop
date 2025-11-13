using UnityEngine;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.mvp;
using com.ab.mvpshop.modules.gold.model;
using com.ab.mvpshop.modules.gold.signals;

namespace com.ab.mvpshop.Modules.gold.definition
{
    [CreateAssetMenu (fileName = "$NAME$GoldChangeAmountCommandDef",menuName = "Commands/Gold change amount")]
    public class GoldChangeAmountCommandSo : CommandSo
    {
        public int ValueToChange;

        public override IModel GetCost(CommandContext ctx) => 
            new Gold(ValueToChange);

        public override void Execute(CommandContext ctx)
        {
            base.Execute(ctx);
            ctx.Signals.Fire(new GoldChangeAmountSignal(ValueToChange));
        }
    }
}