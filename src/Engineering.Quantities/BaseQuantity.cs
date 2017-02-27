namespace Engineering.Quantities
{
    public abstract class BaseQuantity : Quantity
    {
        protected BaseQuantity(string name, string symbol)
            : base(name)
        {
            Symbol = symbol;
        }
        public sealed override string Symbol { get; }

        public override bool Equals(IQuantity other)
        {
            if (other == null)
                return false;
            var otherBase = other as BaseQuantity;
            if (otherBase == null)
                return false;
            return Name.Equals(otherBase.Name)
                && Symbol.Equals(otherBase.Symbol);
        }

        protected sealed override int GetHashCodeImpl()
            => 17*Name.GetHashCode() + Symbol.GetHashCode();
    }
}