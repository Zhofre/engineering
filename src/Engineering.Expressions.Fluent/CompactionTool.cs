using System.Collections.Generic;

namespace Engineering.Expressions.Fluent
{
    internal static class CompactionTool
    {
        public static Expression<T> Compact<T>(Expression<T> expression, bool expandConflictingPrefixes)
            where T : IExpressible
        {
            if (!expression.IsMultiplication())
                return expression;

            var classifiedExpression = expression as IClassifiable<T>;
            var parts = classifiedExpression.GetNumerator();

            // todo: try to compact
            throw new System.NotImplementedException();
        }

    }
}