using System;
using System.Collections.Generic;

namespace Engineering.Expressions
{
    public sealed class InverseExpression<T> : Expression<T>
        where T : IExpressible
    {
        public InverseExpression(Expression<T> expression)
        {
            Content = expression;
        }

        public Expression<T> Content { get; }

        public override bool CanScale => Content.CanScale;

        internal override bool RequiresBrackets => true;

        public override string Representation => $"1/{Content.AutoBracketedRepresentation}";

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new InverseExpression<TOther>(Content.Cast(f));
              
        protected override IEnumerable<Expression<T>> GetDenominatorImpl()
            => new[] { Content };

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => null;

        protected override double GetScaleImpl()
            => 1d;

        protected override double GetExponentImpl() => 1d;
    }
}