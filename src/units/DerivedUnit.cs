using Engineering.Units.Expressions;

namespace Engineering.Units
{
    public class DerivedUnit : Unit, IDerived<IUnit>
    {
        public DerivedUnit(string name, Expression<IUnit> expression)
            : base(name, new DerivedQuantity(name, expression.Cast(x => x.Quantity)))
        {
            Expression = expression;
        }

        public Expression<IUnit> Expression { get; }

        public sealed override string Notation
            => Expression.Representation;
    }
}