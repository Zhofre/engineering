using System.Collections.Generic;

namespace Engineering.Expressions.Fluent
{
    public sealed class ExpressionGroup<T> where T : IExpressible
    {
        public ExpressionGroup(T item, params Expression<T>[] expressions)
        {
            Item = item;
            Expressions = new List<Expression<T>>();
            if (expressions != null)
                Expressions.AddRange(expressions);
        }
        public T Item { get; }

        public List<Expression<T>> Expressions { get; }

        public void Compact()
        {
            // todo implement compaction logic
        }

    }
}