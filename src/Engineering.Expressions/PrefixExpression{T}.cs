using System;
using System.Collections.Generic;
using Engineering.Expressions.Attributes;

namespace Engineering.Expressions
{
    public sealed class PrefixExpression<T> : UnaryExpression<T>, IEquatable<PrefixExpression<T>>
        where T : IExpressible
    {
        public PrefixExpression(Prefix p, Expression<T> expression)
            : base(expression)
        {
            Prefix = p;
        }

        public Prefix Prefix { get; }

        public override string Representation
        {
            get
            {
                if (!CanScale)
                    return Content.Representation;
                var attr = Prefix.GetAttribute<PrefixInformationAttribute>();
                return (attr?.Symbol ?? "") + Content.Representation;
            }
        }

        internal override bool RequiresBrackets => false;

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new PrefixExpression<TOther>(Prefix, Content.Cast(f));

        public override Expression<T> Transform(Func<Expression<T>, Expression<T>> f)
            => new PrefixExpression<T>(Prefix, f(Content));

        protected override IEnumerable<Expression<T>> GetDenominatorImpl()
            => null;

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => new[] { Content };

        protected override double GetScaleImpl()
            => Prefix.Value();

        protected override double GetExponentImpl() => 1d;
        
        public sealed override bool Equals(Expression<T> other)
            => Equals(other as PrefixExpression<T>);

        protected sealed override int GetHashCodeImpl()
            => base.GetHashCodeImpl() + Prefix.GetHashCode();

        public bool Equals(PrefixExpression<T> other)
        {
            if (other == null)
                return false;
            return Content.Equals(other.Content) 
                && (Prefix == other.Prefix);
        }
    }
}