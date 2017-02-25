using System;
using System.Collections.Generic;

namespace Engineering.Expressions
{
    public sealed class EmptyExpression<T> : Expression<T>, IEquatable<EmptyExpression<T>>
        where T : IExpressible
    {
        public override bool CanScale => false;

        public override string Representation => "-";

        internal override bool RequiresBrackets => false;

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new EmptyExpression<TOther>();

        public override Expression<T> Transform(Func<Expression<T>, Expression<T>> f)
            => new EmptyExpression<T>();

        protected override IEnumerable<Expression<T>> GetDenominatorImpl() => null;

        protected override double GetExponentImpl() => 1d;

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => new[] { this };

        protected override double GetScaleImpl() => 1d;

        public override bool Equals(Expression<T> other)
            => Equals(other as EmptyExpression<T>);

        protected sealed override int GetHashCodeImpl()
            => base.GetHashCode();

        public bool Equals(EmptyExpression<T> other)
            => other != null;

    }
}