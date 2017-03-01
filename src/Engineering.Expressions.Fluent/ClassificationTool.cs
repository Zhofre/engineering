using System.Collections.Generic;
using System.Linq;

namespace Engineering.Expressions.Fluent
{
    public static class ClassificationTool
    {

        public static bool IsBaseExpression<T>(this Expression<T> expression, bool expandPrefix) where T : IExpressible
            => expression is EmptyExpression<T>
            || expression is ConstantExpression<T>
            || (!expandPrefix && expression is PrefixExpression<T>);

        public static bool IsMultiplication<T>(this Expression<T> expression) where T : IExpressible
            => expression is MultiplicationExpression<T>
            || expression is MultiplicationSequenceExpression<T>;

        public static bool IsModifiedConstantExpression<T>(this Expression<T> expression) where T : IExpressible
            => expression is ConstantExpression<T>
            || (expression is UnaryExpression<T>
            ? IsModifiedConstantExpression((expression as UnaryExpression<T>).Content)
            : false);

        public static ConstantExpression<T> ExtractConstantExpression<T>(this Expression<T> expression) where T : IExpressible
            => (expression?.IsModifiedConstantExpression() ?? false)
            ? (expression is ConstantExpression<T>
                ? expression as ConstantExpression<T>
                : (expression is UnaryExpression<T>
                    ? ExtractConstantExpression((expression as UnaryExpression<T>).Content)
                    : null))
            : null;

        /// <summary>
        ///     Creates <see cref="ExpressionGroup{T}" /> objects for each modified constant expression
        ///     All other expressions are bundled in an empty item group and are the last item in the 
        ///     return list.
        /// </summary>
        /// <param name="expressions"></param>
        /// <returns>List of <see cref="ExpressionGroup{T}" /> per modified constant expression</returns>
        public static List<ExpressionGroup<T>> CreateExpressionGroups<T>(this IEnumerable<Expression<T>> expressions) where T : class, IExpressible
        {
            var result = new List<ExpressionGroup<T>>();
            var garbage = new ExpressionGroup<T>(null);
            foreach (var x in expressions)
            {
                var item = x.ExtractConstantExpression()?.Content;
                var group = item == null
                    ? garbage
                    : result.FirstOrDefault(g => g.Item.Equals(item));
                if (group == null)
                    result.Add(group = new ExpressionGroup<T>(item));
                group.Expressions.Add(x);
            }
            if (garbage.Expressions.Count > 0)
                result.Add(garbage);
            return result;
        }

    }
}