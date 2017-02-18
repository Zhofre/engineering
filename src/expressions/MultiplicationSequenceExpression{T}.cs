using System;
using System.Collections.Generic;
using System.Linq;

namespace Engineering.Expressions
{
    public sealed class MultiplicationSequenceExpression<T> : Expression<T>
        where T : IExpressible
    {
        public MultiplicationSequenceExpression(Expression<T> first, Expression<T> second, params Expression<T>[] other)
            : this(new List<Expression<T>> { first, second }.Union(other))
        {
        }

        public MultiplicationSequenceExpression(IEnumerable<Expression<T>> expressions)
        {
            if (expressions.Count() < 2)
                throw new ArgumentException("Requires at least two expressions");
            Content = expressions.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Expression<T>> Content { get; }

        public override bool CanPrefix => Content.First().CanPrefix;

        internal override bool RequiresBrackets => true;

        public override string Representation
            => Content
                .Select(x => x.AutoBracketedRepresentation)
                .Aggregate((t, n) => t + "*" + n);

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new MultiplicationSequenceExpression<TOther>(Content.Select(x => x.Cast(f)));

    }
}