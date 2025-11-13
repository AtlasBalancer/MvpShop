using UnityEngine;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.modules.location
{
    [CreateAssetMenu (fileName = "$NAME$LocationChangeCommandDef",menuName = "Commands/Location change")]
    public class LocationChangeCommandSo : CommandSo
    {
        public string ValueToChange;

        public override IModel GetCost(CommandContext ctx) => 
            new Location(ValueToChange);

        public override void Execute(CommandContext ctx)
        {
            base.Execute(ctx);
            ctx.Signals.Fire(new LocationChangeSignal(ValueToChange));
        }
    }
}