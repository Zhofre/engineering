using System;

namespace Engineering.Expressions
{
    public sealed class MultiplicationExpression<T> : BinaryExpression<T>
        where T : IExpressible
    {
        public MultiplicationExpression(Expression<T> lhs, Expression<T> rhs)
            : base(lhs, rhs)
        {
        }

        internal override bool RequiresBrackets => true;

        protected override string OperatorSymbol => "*";

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new MultiplicationExpression<TOther>(LeftHandSide.Cast(f), RightHandSide.Cast(f));
    }
}