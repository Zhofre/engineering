using System;
using System.Collections.Generic;

namespace Engineering.Expressions
{
    public sealed class EmptyExpression<T> : Expression<T>
        where T : IExpressible
    {
        public override bool CanScale => false;

        public override string Representation => "-";

        internal override bool RequiresBrackets => false;

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new EmptyExpression<TOther>();

        protected override IEnumerable<Expression<T>> GetDenominatorImpl() => null;

        protected override double GetExponentImpl() => 1d;

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => new[] { this };

        protected override double GetScaleImpl() => 1d;
    }
}