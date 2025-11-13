using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.mvp;
using UnityEngine;

namespace com.ab.mvpshop.modules.location
{
    [CreateAssetMenu (fileName = "$NAME$LocationCheckCommandDef",menuName = "Commands/Location check")]
    public class LocationCheckCommandSo : CommandSo
    {
        public string ValueToChange;

        public override IModel GetCost(CommandContext ctx) => 
            new Location(ValueToChange);

        public override void Execute(CommandContext ctx)
        { }
    }
}