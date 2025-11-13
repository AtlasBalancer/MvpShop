using System;
using System.Collections.Generic;

namespace com.ab.mvpshop.core.command
{
    public class CommandInvoker
    {
        readonly CommandContext _ctx;
        readonly Dictionary<Type, ICommandCanExecute> _processors;
        
        public CommandInvoker(CommandContext ctx)
        {
            _ctx = ctx;
            _processors = new();
        }

        public void Registry(Type key, ICommandCanExecute processor) => 
            _processors.Add(key, processor);

        public bool CanExecute(CommandCostComposite @ref)
        {
            foreach (var item in @ref.Cost)
            {
                var execute = _processors[item.Key]
                    .CanExecute(_ctx, item.Value);
                if (execute == false)
                    return false;
            }

            return true;
        }
    }
}