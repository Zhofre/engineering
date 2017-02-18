using System;

namespace Engineering.Expressions.Fluent
{
    public static class Operators
    {
        /// <summary>
        ///     Expands expression
        /// </summary>
        /// <param name="expression">Expression to expand</param>
        /// <param name="option">Determines the aggresiveness of the expansion</param>
        /// <returns>New expanded expression</returns>
        public static Expression<T> Expand<T>(this Expression<T> expression, ExpandMode option = ExpandMode.Normal)
            where T : IExpressible
        {
            switch(option)
            {
                case ExpandMode.Normal : return ExpandTool.Normal(expression);
                case ExpandMode.Aggresive : return ExpandTool.Aggresive(expression);
                default: throw new NotImplementedException($"Unknown expansion model {option}");
            }
        }
    }
}