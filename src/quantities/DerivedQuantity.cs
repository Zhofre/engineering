using Engineering.Expressions;

namespace Engineering.Quantities
{
    public class DerivedQuantity : Quantity, IDerived<IQuantity>
    {
        private string _customSymbol;

        public DerivedQuantity(string name, Expression<IQuantity> expression)
            : this(name, null, expression)
        {
            
        }

        public DerivedQuantity(string name, string symbol, Expression<IQuantity> expression)
            : base(name)
        {
            _customSymbol = symbol;
            Expression = expression;
        }

        public Expression<IQuantity> Expression { get; }

        public sealed override string Symbol
            => _customSymbol ?? Expression.Representation;
    }
}