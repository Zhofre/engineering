using System.Collections.Generic;

namespace Engineering.Expressions.Fluent
{
    internal static class CompactionTool
    {
        public static Expression<T> Compact<T>(Expression<T> expression, bool expandConflictingPrefixes)
            where T : class, IExpressible
        {
            if (!expression.IsMultiplication())
                return expression;

            var classifiedExpression = expression as IClassifiable<T>;
            var parts = classifiedExpression.GetNumerator();

            var groups = parts.CreateExpressionGroups<T>();
            foreach(var g in groups)
                g.Compact(expandConflictingPrefixes);            
            var newRange = new List<Expression<T>>();
            foreach(var g in groups)
                newRange.AddRange(g.Expressions);
            
            switch (newRange.Count)
            {
                case 0: return new EmptyExpression<T>();
                case 1: return newRange[0];
                case 2: return new MultiplicationExpression<T>(newRange[0], newRange[1]);
                default: return new MultiplicationSequenceExpression<T>(newRange);
            }
        }

    }
}