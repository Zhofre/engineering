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
            switch (options)
            {
                case ExpandOptions.Normal: return ExpandTool.Normal(expression);
                case ExpandOptions.Normal | ExpandOptions.PrefixToScale: return ExpandTool.Normal(expression, true);
                case ExpandOptions.Aggressive: return ExpandTool.Aggressive(expression);
                case ExpandOptions.Aggressive | ExpandOptions.PrefixToScale: return ExpandTool.Aggressive(expression, true);
                default: throw new NotImplementedException($"Unknown expansion options combination {options}");
            }
        }

        public static Expression<T> Simplify<T>(this Expression<T> expression, SimplifyOptions options = SimplifyOptions.Normal)
            where T : IExpressible
        {
            var res = SimplifyTool.Normal(expression);

            if ((options & SimplifyOptions.SimplifyPrefixes) == SimplifyOptions.SimplifyPrefixes)
            {
                throw new NotImplementedException();
            }

            return res;
        }
    }
}