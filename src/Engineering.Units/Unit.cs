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

        public abstract bool Equals(IUnit other);

        public bool Equals(IExpressible other)
            => Equals(other as IUnit);

        public sealed override int GetHashCode() => GetHashCodeImpl();
        protected abstract int GetHashCodeImpl();

        public sealed override bool Equals(object other)
            => Equals(other as IUnit);

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