using System;
using System.Collections.Generic;
using System.Linq;

namespace Engineering.Expressions
{
    public sealed class MultiplicationSequenceExpression<T> : SequenceExpression<T>
        where T : IExpressible
    {
        public MultiplicationSequenceExpression(Expression<T> first, Expression<T> second, params Expression<T>[] other)
            : this(new List<Expression<T>> { first, second }.Concat(other))
        {
        }

        public MultiplicationSequenceExpression(IEnumerable<Expression<T>> expressions)
            : base(expressions)
        {
            if (expressions.Count() < 2)
                throw new ArgumentException("Requires at least two expressions");
        }

        internal override bool RequiresBrackets => true;

        public override string Representation
            => Content
                .Select(x => x.AutoBracketedRepresentation)
                .Aggregate((t, n) => t + "*" + n);

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new MultiplicationSequenceExpression<TOther>(Content.Select(x => x.Cast(f)));
   
        public override Expression<T> Transform(Func<Expression<T>, Expression<T>> f)
            => new MultiplicationSequenceExpression<T>(Content.Select(x => f(x)));

        protected override IEnumerable<Expression<T>> GetDenominatorImpl()
            => null;

        protected override IEnumerable<Expression<T>> GetNumeratorImpl()
            => Content;

        protected override double GetScaleImpl()
            => 1d;

        protected override double GetExponentImpl() => 1d;
    }
}