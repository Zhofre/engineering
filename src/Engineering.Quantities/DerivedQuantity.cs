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

        public bool HasCustomRepresentation => _customSymbol != null;

        public override bool Equals(IQuantity other)
        {
            if (other == null)
                return false;
            var otherDerived = other as DerivedQuantity;
            if (otherDerived == null)
                return false;

            var res = true;
            if (_customSymbol == null)
                res &= otherDerived._customSymbol == null;
            if (otherDerived._customSymbol == null)
                res &= _customSymbol == null;
            return res && Expression.Equals(otherDerived.Expression)
                && Name.Equals(otherDerived.Name);
        }

        protected override int GetHashCodeImpl()
            => Expression.GetHashCode() + 21*Name.GetHashCode();
    }
}