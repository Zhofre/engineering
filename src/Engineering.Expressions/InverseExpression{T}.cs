using System;
using System.Collections.Generic;

namespace Engineering.Expressions
{
    public sealed class InverseExpression<T> : UnaryExpression<T>
        where T : IExpressible
    {
        public InverseExpression(Expression<T> expression)
            : base (expression)
        {
        }

        internal override bool RequiresBrackets => true;

        public override string Representation => $"1/{Content.AutoBracketedRepresentation}";

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new InverseExpression<TOther>(Content.Cast(f));

        public override Expression<T> Transform(Func<Expression<T>, Expression<T>> f)
            => new InverseExpression<T>(f(Content));
              
        protected override IEnumerable<Expression<T>> GetDenominatorImpl()
            => new[] { Content };

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => null;

        protected override double GetScaleImpl()
            => 1d;

        protected override double GetExponentImpl() => 1d;
    }
}