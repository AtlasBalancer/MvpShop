using System;
using UnityEngine;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.mvp;
using com.ab.mvpshop.modules.vip.model;

namespace com.ab.mvpshop.modules.vip.Interaction
{
    [CreateAssetMenu (fileName = "$NAME$VipCheckCommandDef",menuName = "Commands/Vip check amount")]
    public class VipCheckCommandSo : CommandSo
    {
        public override IModel GetCost(CommandContext ctx) => new Vip(TimeSpan.Zero);

        public override void Execute(CommandContext ctx)
        { }
    }
}