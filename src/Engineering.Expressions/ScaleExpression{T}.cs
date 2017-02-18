using System;
using System.Globalization;

namespace Engineering.Expressions
{
    public sealed class ScaleExpression<T> : Expression<T>
        where T : IExpressible
    {
        public ScaleExpression(double scale, Expression<T> expression)
        {
            Scale = scale;
            Content = expression;
        }

        public double Scale { get; }

        public Expression<T> Content { get; }

        public override bool CanScale => Content.CanScale;

        internal override bool RequiresBrackets => !Utility.Equals(Scale, 1.0);

        public override string Representation
        {
            get
            {
                if (!CanScale || Utility.Equals(Scale, 1.0))
                    return Content.Representation;   
                return Scale.ToString(CultureInfo.InvariantCulture) + "*" + Content.AutoBracketedRepresentation;
            }
        }

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new ScaleExpression<TOther>(Scale, Content.Cast(f));
    }
}