using System.Collections.Generic;

namespace Engineering.Expressions.Fluent
{
    internal static class ParenthesesTool
    {

        /// <summary>
        ///     Method checks a multiplication and removes parenthesis from direct children
        ///     ((a*b)*(c*(d*e)*f)) -> (a*b*c*(d*e)*f)
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>expression with one level of eliminated parentheses</returns>
        public static Expression<T> EliminateParentheses<T>(Expression<T> expression)
            where T : IExpressible
        {
            if (!expression.IsMultiplication())
                return expression;

            // it is a multiplication so we can get the numerator parts
            var classifiedExpression = expression as IClassifiable<T>;
            var parts = classifiedExpression.GetNumerator();
            var result = new List<Expression<T>>();
            foreach (var p in parts)
            {
                if (!p.IsMultiplication())
                {
                    result.Add(p);
                    continue;
                }
                
                var classifiedExpression2 = p as IClassifiable<T>;
                result.AddRange(classifiedExpression2.GetNumerator());
            }
            return new MultiplicationSequenceExpression<T>(result);
        }

    }
}