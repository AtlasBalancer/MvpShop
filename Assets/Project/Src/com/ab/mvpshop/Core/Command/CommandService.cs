using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace com.ab.mvpshop.core.command
{
    public class CommandService : ICommandService
    {
        readonly CommandContext _ctx;

        public CommandService(CommandContext ctx) => 
            _ctx = ctx;

        public void Execute(List<ICommand> executions) => 
            executions.ForEach(item => item.Execute(_ctx));

        public CommandCostComposite CastSoToCommandCostAction(List<ScriptableObject> soActions) => 
            new(_ctx, soActions.OfType<ICommand>().ToList());
    }
}