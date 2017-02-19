using System;
using System.Collections.Generic;
using System.Globalization;

namespace Engineering.Expressions
{
    public sealed class ScaleExpression<T> : UnaryExpression<T>, IEquatable<ScaleExpression<T>>
        where T : IExpressible
    {
        public ScaleExpression(double scale, Expression<T> expression)
            : base(expression)
        {
            Scale = scale;
        }

        public double Scale { get; }

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
               
        public override Expression<T> Transform(Func<Expression<T>, Expression<T>> f)
            => new ScaleExpression<T>(Scale, f(Content));

        protected override IEnumerable<Expression<T>> GetDenominatorImpl()
            => null;

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => new[] { Content };

        protected override double GetScaleImpl()
            => Scale;

        protected override double GetExponentImpl() => 1d;
        
        public sealed override bool Equals(Expression<T> other)
            => Equals(other as ScaleExpression<T>);

        protected sealed override int GetHashCodeImpl()
            => base.GetHashCodeImpl() + Scale.GetHashCode();

        public bool Equals(ScaleExpression<T> other)
        {
            if (other == null)
                return false;
            return Content.Equals(other.Content) && Utility.Equals(Scale, other.Scale);
        }
    }
}