using Engineering.Units.Expressions;

namespace Engineering.Units
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
    }
}