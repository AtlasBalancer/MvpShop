namespace com.ab.mvpshop.core.mvp
{
    public abstract class AmountModel<T> : IModel
    {
        public T Amount;

        public AmountModel(T amount) => 
            Amount = amount;

        public abstract void Combine(IModel model);
    }
}