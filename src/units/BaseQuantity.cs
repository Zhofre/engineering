namespace Engineering.Units
{
    public abstract class BaseQuantity : Quantity
    {
        protected BaseQuantity(string name, string symbol)
            : base(name)
        {
            Symbol = symbol;
        }
        public sealed override string Symbol { get; }
    }
}