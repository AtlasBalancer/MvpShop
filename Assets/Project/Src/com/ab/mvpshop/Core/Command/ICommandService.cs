using UnityEngine;
using System.Collections.Generic;

namespace com.ab.mvpshop.core.command
{
    public interface ICommandService
    {
        void Execute(List<ICommand> executions);
        CommandCostComposite CastSoToCommandCostAction(List<ScriptableObject> soActions);
    }
}