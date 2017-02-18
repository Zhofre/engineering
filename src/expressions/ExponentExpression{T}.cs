using System;
using System.Globalization;

namespace Engineering.Expressions
{
    public sealed class ExponentExpression<T> : Expression<T>
        where T : IExpressible
    {
        public ExponentExpression(Expression<T> expression, double exponent)
        {
            Content = expression;
            Exponent = exponent;
        }

        public double Exponent { get; }

        public Expression<T> Content { get; }

        public override bool CanPrefix => Content.CanPrefix;

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
    }
}