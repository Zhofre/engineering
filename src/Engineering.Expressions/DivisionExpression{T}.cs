using System;
using System.Collections.Generic;

namespace Engineering.Expressions
{
    public sealed class DivisionExpression<T> : BinaryExpression<T>
        where T : IExpressible
    {
        public DivisionExpression(Expression<T> lhs, Expression<T> rhs)
            : base(lhs, rhs)
        {
        }

        internal override bool RequiresBrackets => true;

        protected override string OperatorSymbol => "/";

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new DivisionExpression<TOther>(LeftHandSide.Cast(f), RightHandSide.Cast(f));

        protected override IEnumerable<Expression<T>> GetDenominatorImpl()
            => new[] { RightHandSide };

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => new[] { LeftHandSide };

        protected override double GetScaleImpl()
            => 1d;

        protected override double GetExponentImpl() => 1d;
    }
}