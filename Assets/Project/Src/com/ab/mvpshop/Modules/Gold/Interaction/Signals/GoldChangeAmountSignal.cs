namespace com.ab.mvpshop.modules.gold.signals
{
    public class GoldChangeAmountSignal
    {
        public int ValueToChange;

        public GoldChangeAmountSignal(int value) => 
            ValueToChange = value;
    }
}