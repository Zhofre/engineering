using System;
using System.Collections.Generic;

namespace Engineering.Expressions
{
    public sealed class ConstantExpression<T> : Expression<T>, IEquatable<ConstantExpression<T>>
        where T : IExpressible
    {
        public ConstantExpression(T content)
        {
            Content = content;
        }

        public T Content { get; }

        public override bool CanScale => Content.CanScale;

        public override string Representation => Content.Representation;

        internal override bool RequiresBrackets => false;

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new ConstantExpression<TOther>(f(Content));

        public override Expression<T> Transform(Func<Expression<T>, Expression<T>> f)
            => new ConstantExpression<T>(Content); // <- does nothing

        protected override IEnumerable<Expression<T>> GetDenominatorImpl()
            => null;

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => new[] { this };

        protected override double GetScaleImpl() => 1d;

        protected override double GetExponentImpl() => 1d;

        public sealed override bool Equals(Expression<T> other)
            => Equals(other as ConstantExpression<T>);

        protected sealed override int GetHashCodeImpl()
            => Content.GetHashCode();

        public bool Equals(ConstantExpression<T> other)
        {
            if (other == null)
                return false;
            return Content.Equals(other.Content);
        }
    }
}