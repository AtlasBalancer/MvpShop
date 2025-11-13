using System;
using System.Collections.Generic;
using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.core.command
{
    public interface ICommandCost 
    {
        Dictionary<Type, IModel> Cost { get; }
    }
}