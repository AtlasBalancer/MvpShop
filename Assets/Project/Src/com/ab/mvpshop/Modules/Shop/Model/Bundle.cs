using com.ab.mvpshop.core.command;

namespace com.ab.mvpshop.modules.shop.model
{
    public class Bundle 
    {
        public readonly int Id;
        
        public readonly string Message;
        public readonly CommandCostComposite Conditions;
        public readonly CommandCostComposite Results;

        public Bundle(int id, CommandCostComposite conditions, CommandCostComposite results, string message)
        {
            Id = id;
            Message = message;
            Results = results;
            Conditions = conditions;
        }
    }
}