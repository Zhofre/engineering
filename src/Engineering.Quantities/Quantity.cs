using Engineering.Expressions;

namespace Engineering.Quantities
{
    public abstract class Quantity : IQuantity
    {
        protected Quantity(string name)
        {
            Name = name;
        }

        bool IExpressible.CanScale => false;
        string IExpressible.Representation => Symbol;
        public string Name { get; }
        public abstract string Symbol { get; }

        public abstract bool Equals(IQuantity other);

        public bool Equals(IExpressible other)
            => Equals(other as IQuantity);

        public sealed override int GetHashCode() => GetHashCodeImpl();
        protected abstract int GetHashCodeImpl();

        public sealed override bool Equals(object other)
            => Equals(other as IQuantity);
    }
}