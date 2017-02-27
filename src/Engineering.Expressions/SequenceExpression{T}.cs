using System.Collections.Generic;
using System.Linq;

namespace Engineering.Expressions
{
    public abstract class SequenceExpression<T> : Expression<T>
        where T : IExpressible
    {
        protected SequenceExpression(IEnumerable<Expression<T>> expressions)
        {
           Content = expressions.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Expression<T>> Content { get; }

        public override bool CanScale => Content.First().CanScale;

        protected override int GetHashCodeImpl()
            => Content.GetHashCode();

    }
    
}