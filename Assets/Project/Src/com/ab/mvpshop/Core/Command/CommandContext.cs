using Zenject;

namespace com.ab.mvpshop.core.command
{
    public class CommandContext
    {
        public SignalBus Signals;

        public CommandContext(SignalBus signals) => 
            Signals = signals;
    }
}