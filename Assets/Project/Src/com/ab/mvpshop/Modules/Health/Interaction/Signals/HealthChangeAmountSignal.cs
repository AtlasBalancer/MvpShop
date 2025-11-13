namespace com.ab.mvpshop.modules.health.signals
{
    public class HealthChangeAmountSignal 
    {
        public int ValueToChange;

        public HealthChangeAmountSignal(int value) => 
            ValueToChange = value;
    }
}