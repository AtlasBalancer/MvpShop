using System;
using UnityEngine;
using com.ab.mvpshop.core.command;
using com.ab.mvpshop.core.mvp;
using com.ab.mvpshop.modules.vip.interaction.Signals;
using com.ab.mvpshop.modules.vip.model;

namespace com.ab.mvpshop.modules.vip.Interaction
{
    
    [CreateAssetMenu (fileName = "$NAME$VipChangeAmountCommandDef",menuName = "Commands/Vip change amount")]
    public class VipChangeCommandSo : CommandSo
    {
        public int ChangeSeconds;

        public override IModel GetCost(CommandContext ctx) => 
            new Vip(TimeSpan.FromSeconds(ChangeSeconds));

        public override void Execute(CommandContext ctx)
        {
            base.Execute(ctx);
            ctx.Signals.Fire(new VipChangeSignal(TimeSpan.FromSeconds(ChangeSeconds)));
        }
    }
}