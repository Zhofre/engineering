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
        public static Expression<T> Expand<T>(this Expression<T> expression, ExpandOptions options = ExpandOptions.Normal)
            where T : IExpressible
        {
            switch(options)
            {
                case ExpandOptions.Normal : return ExpandTool.Normal(expression);
                case ExpandOptions.Normal | ExpandOptions.Prefix : return ExpandTool.Normal(expression, true);
                case ExpandOptions.Aggresive : return ExpandTool.Aggresive(expression);
                case ExpandOptions.Aggresive | ExpandOptions.Prefix : return ExpandTool.Normal(expression, true);
                default: throw new NotImplementedException($"Unknown expansion options combination {options}");
            }
        }
    }
}