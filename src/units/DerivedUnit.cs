using Engineering.Units.Expressions;

namespace Engineering.Units
{
    public class DerivedUnit : Unit, IDerived<IUnit>
    {
        private string _customNotation;

        public DerivedUnit(string name, Expression<IUnit> expression)
            : this(name, null, expression)
        {
            
        }

        public DerivedUnit(string name, string notation, Expression<IUnit> expression)
            : this(name, notation, expression, null)
        {
            
        }

        public DerivedUnit(string name, string notation, Expression<IUnit> expression, string quantitySymbol)
            : base(name, new DerivedQuantity(name, quantitySymbol, expression.Cast(x => x.Quantity)))
        {
            _customNotation = notation;
            Expression = expression;
        }

        public Expression<IUnit> Expression { get; }

        public sealed override string Notation
            => _customNotation ?? Expression.Representation;
    }
}