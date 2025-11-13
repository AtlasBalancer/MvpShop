using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.core.command
{
    public interface ICommandCanExecute
    {
        /// <summary>
        /// Return <c>true</c> if command available to execute
        /// </summary>
        bool CanExecute(CommandContext ctx, IModel model);
    }
}