using System;
using System.Collections.Generic;

namespace Engineering.Expressions
{
    public sealed class MultiplicationExpression<T> : BinaryExpression<T>, IEquatable<MultiplicationExpression<T>>
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
               
        public override Expression<T> Transform(Func<Expression<T>, Expression<T>> f)
            => new MultiplicationExpression<T>(f(LeftHandSide), f(RightHandSide));

        protected override IEnumerable<Expression<T>> GetDenominatorImpl()
            => null;

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => new[] { LeftHandSide, RightHandSide };

        protected override double GetScaleImpl()
            => 1d;

        protected override double GetExponentImpl() => 1d;
        
        public override bool Equals(Expression<T> other)
            => Equals(other as MultiplicationExpression<T>);

        public bool Equals(MultiplicationExpression<T> other)
        {
            if (other == null)
                return false;
            return LeftHandSide.Equals(other.LeftHandSide)
                && RightHandSide.Equals(other.RightHandSide);
        }
    }
}