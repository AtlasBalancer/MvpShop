using System;
using System.Collections.Generic;
using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.core.command
{
    public class CommandCostComposite : ICommandCost
    {
        public Dictionary<Type, IModel> Cost { get; }
        public readonly List<ICommand> Executions;

        public CommandCostComposite(CommandContext ctx, List<ICommand> executions)
        {
            Executions = executions;
            Cost = new();

            foreach (var item in executions)
            {
                var cost = item.GetCost(ctx);
                if(cost == default)
                    continue;
                
                var key = cost.GetType();
                
                if (Cost.ContainsKey(key))
                    Cost[key].Combine(cost);
                else
                    Cost.Add(key, cost);
            }
        }
    }
}