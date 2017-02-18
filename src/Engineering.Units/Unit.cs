using Engineering.Expressions;
using Engineering.Quantities;

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

        public static Expression<IUnit> operator *(Prefix p, Unit u)
        {
            var derU = u as DerivedUnit;
            return new PrefixExpression<IUnit>(p,
                derU == null
                    ? new ConstantExpression<IUnit>(u)
                    : derU.Expression);
        }
    }
}