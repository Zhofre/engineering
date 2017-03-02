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
            var canon = Expressions.Select(x => x.CanonicalForm()).ToList();
            // check if prefixes conform
            var conformingPrefixes = true;
            var testPrefix = Prefix.none;
            var firstItemChecked = false;
            var i = 0;
            while (conformingPrefixes && (i < canon.Count))
            {
                var c = canon[i++] as ScaleExpression<T>;
                if (!c.IsConstantOrUnaryExpression())
                    continue;

                // get the prefixe
                var cExp = c.Content as ExponentExpression<T>;
                var testExpr = cExp.Content;
                if (testExpr is InverseExpression<T>)
                    testExpr = (testExpr as InverseExpression<T>).Content;
                var pExpr = testExpr as PrefixExpression<T>;
                if (pExpr == null)
                    throw new System.Exception("Unexpected structure of canonical form");

                var thisPrefix = pExpr.Prefix;
                if (!firstItemChecked)
                {
                    testPrefix = thisPrefix;
                    firstItemChecked = true;
                    continue;
                }
                conformingPrefixes = thisPrefix == testPrefix;
            }

            // fix prefix issues
            if(!conformingPrefixes && expandPrefixesIfConflicting)
                for(i = 0; i < canon.Count; i++)
                {
                    var c = canon[i];
                    if (!c.IsConstantOrUnaryExpression())
                        continue;
                    canon[i] = c.ReplacePrefixes(Prefix.none).CanonicalForm();
                }

            // create groups with similar prefixes (compaction candidates)
            // todo: create group tool to do exactly this... some functionality of expression group create could be in this as well


            // todo: refactor to make this unit testable


            // todo: build new expansion option that preserves simple prefixes


            // todo implement compaction logic
        }

    }
}