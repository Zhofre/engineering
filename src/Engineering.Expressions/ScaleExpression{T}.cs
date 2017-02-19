using System;
using System.Collections.Generic;
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
                return Scale.ToString("E3", CultureInfo.InvariantCulture) + "*" + Content.AutoBracketedRepresentation;
            }
        }

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new ScaleExpression<TOther>(Scale, Content.Cast(f));
               
        protected override IEnumerable<Expression<T>> GetDenominatorImpl()
            => null;

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => new[] { Content };

        protected override double GetScaleImpl()
            => Scale;

        protected override double GetExponentImpl() => 1d;
    }
}