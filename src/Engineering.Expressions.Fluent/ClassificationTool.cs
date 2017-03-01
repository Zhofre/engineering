using System.Collections.Generic;

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
            ||(expression is UnaryExpression<T>
            ? IsModifiedConstantExpression((expression as UnaryExpression<T>).Content)
            : false);
        
        /// <summary>
        ///     Creates <see cref="ExpressionGroup{T}" /> objects for each modified constant expression
        ///     All other expressions are bundled in an empty item group and are the last item in the 
        ///     return list.
        /// </summary>
        /// <param name="expressions"></param>
        /// <returns>List of <see cref="ExpressionGroup{T}" /> per modified constant expression</returns>
        public static List<ExpressionGroup<T>> CreateExpressionGroups<T>(this IEnumerable<Expression<T>> expressions) where T: IExpressible
        {            
            var result = new List<ExpressionGroup<T>>();
            foreach(var x in expressions)
            {
                    // todo

            }
            return result;            
        }

    }
}