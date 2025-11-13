using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.core.command
{
    public interface ICommand
    {

        IModel GetCost(CommandContext ctx);

        /// <summary>
        /// Execute business logic
        /// </summary>
        void Execute(CommandContext ctx);
    }
}