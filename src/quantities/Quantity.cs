using Engineering.Expressions;

namespace Engineering.Quantities
{
    public abstract class Quantity : IQuantity
    {
        protected Quantity(string name)
        {
            Name = name;
        }

        bool IExpressible.CanPrefix => false;
        string IExpressible.Representation => Symbol;
        public string Name { get; }
        public abstract string Symbol { get; }
    }
}