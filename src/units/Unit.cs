using Engineering.Units.Expressions;

namespace Engineering.Units
{
    public abstract class Unit : IUnit
    {
        protected Unit(string name, IQuantity quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        bool IExpressible.CanScale => true;
        string IExpressible.Representation => Notation;

        public string Name { get; }
        public abstract string Notation { get; }
        public IQuantity Quantity { get; }
    }
}