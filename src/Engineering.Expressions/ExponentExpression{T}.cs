using System;
using System.Collections.Generic;
using System.Globalization;

namespace Engineering.Expressions
{
    public sealed class ExponentExpression<T> : UnaryExpression<T>, IEquatable<ExponentExpression<T>>
        where T : IExpressible
    {
        public ExponentExpression(Expression<T> expression, double exponent)
            : base(expression)
        {
            Exponent = exponent;
        }

        public double Exponent { get; }

        internal override bool RequiresBrackets => false;

        public override string Representation
        {
            get
            {
                if (Utility.Equals(Exponent, 1.0))
                    return Content.Representation;                
                return Content.AutoBracketedRepresentation +  "^" + Exponent.ToString(CultureInfo.InvariantCulture);
            }
        }

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new ExponentExpression<TOther>(Content.Cast(f), Exponent);

        public override Expression<T> Transform(Func<Expression<T>, Expression<T>> f)
            => new ExponentExpression<T>(f(Content), Exponent);
            
        protected override IEnumerable<Expression<T>> GetDenominatorImpl() => null;

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => new[] { Content };

        protected override double GetScaleImpl() => 1d;

        protected override double GetExponentImpl() => Exponent;
        
        public sealed override bool Equals(Expression<T> other)
            => Equals(other as ExponentExpression<T>);

        protected sealed override int GetHashCodeImpl()
            => base.GetHashCodeImpl() + Exponent.GetHashCode();

        public bool Equals(ExponentExpression<T> other)
        {
            if (other == null)
                return false;
            return Content.Equals(other.Content) && Utility.Equals(Exponent, other.Exponent);
        }
    }
}