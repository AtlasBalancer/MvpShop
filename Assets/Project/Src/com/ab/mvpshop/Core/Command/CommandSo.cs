using UnityEngine;
using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.core.command
{
    /// <summary>
    /// Base ScriptableObject definition for the Command pattern.
    /// </summary>
    public abstract class CommandSo : ScriptableObject, ICommand
    {
        public virtual IModel GetCost(CommandContext ctx) => default;

        public virtual void Execute(CommandContext ctx) { }
    }
}