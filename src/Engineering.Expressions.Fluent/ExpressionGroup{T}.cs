using System.Collections.Generic;
using System.Linq;

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

        public void Compact(bool expandPrefixesIfConflicting)
        {
            // recognize different unary items and compact them
            var result = Expressions.Select(x => x.Expand(ExpandOptions.Aggressive));


          // todo: build new expansion option that preserves simple prefixes


            // todo implement compaction logic
        }

    }
}