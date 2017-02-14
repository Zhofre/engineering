using System;
using System.Globalization;

namespace Engineering.Units.Expressions
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
        public override string Representation
        {
            get
            {
                if (Utility.Equals(Exponent, 1.0))
                    return Content.Representation;

                // convert exponent
                var exponentString = "^" + Exponent.ToString(CultureInfo.InvariantCulture);

                var constContent = Content as ConstantExpression<T>;
                if (constContent != null)
                    return constContent.Representation + exponentString;

                var prefixContent = Content as PrefixExpression<T>;
                if (prefixContent != null)
                    return prefixContent.Representation + exponentString; // // no need for brackets for [mm], [kg], etc.

                return "(" + Content.Representation + ")" + exponentString;
            }
        }

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new ExponentExpression<TOther>(Content.Cast(f), Exponent);
    }
}