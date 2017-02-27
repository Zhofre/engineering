using Engineering.Expressions;
using Engineering.Quantities;

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

        public bool HasCustomRepresentation => _customNotation != null;
        
        public override bool Equals(IUnit other)
        {
            if (other == null)
                return false;
            var otherDerived = other as DerivedUnit;
            if (otherDerived == null)
                return false;

            var res = true;
            if (_customNotation == null)
                res &= otherDerived._customNotation == null;
            if (otherDerived._customNotation == null)
                res &= _customNotation == null;
            return res && Expression.Equals(otherDerived.Expression)
                && Name.Equals(otherDerived.Name)
                && Quantity.Equals(otherDerived.Quantity);
        }

        protected override int GetHashCodeImpl()
            => Expression.GetHashCode() + 21*Name.GetHashCode() + 17*Quantity.GetHashCode();

    }
}