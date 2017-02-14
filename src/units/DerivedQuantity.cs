using Engineering.Units.Expressions;

namespace Engineering.Units
{
    public class DerivedQuantity : Quantity, IDerived<IQuantity>
    {
        public DerivedQuantity(string name, Expression<IQuantity> expression)
            : base(name)
        {
            Expression = expression;
        }

        public Expression<IQuantity> Expression { get; }

        public sealed override string Symbol
            => Expression.Representation;
    }
}