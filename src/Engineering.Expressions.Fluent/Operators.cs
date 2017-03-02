using System;
using System.Collections.Generic;
using System.Linq;

namespace Engineering.Expressions.Fluent
{
    public static class Operators
    {
        public static ConstantExpression<T> Constant<T>(this T content) where T : IExpressible
            => new ConstantExpression<T>(content);
            
        public static DivisionExpression<T> DivideBy<T>(this Expression<T> expression, Expression<T> other) where T : IExpressible
            => new DivisionExpression<T>(expression, other);

        public static ExponentExpression<T> Pow<T>(this Expression<T> expression, double exponent) where T : IExpressible
            => new ExponentExpression<T>(expression, exponent);

        public static ExponentExpression<T> RaiseTo<T>(this Expression<T> expression, double exponent) where T : IExpressible
            => new ExponentExpression<T>(expression, exponent);

        public static ExponentExpression<T> Exponent<T>(this Expression<T> expression, double exponent) where T : IExpressible
            => new ExponentExpression<T>(expression, exponent);


        public static Expression<T> AutoScale<T>(this Expression<T> expression, double scale) where T : IExpressible
            => Utility.Equals(scale, 1d)
                ? expression
                : new ScaleExpression<T>(scale, expression);

        public static Expression<T> AutoExponent<T>(this Expression<T> expression, double exponent) where T : IExpressible
            => expression is EmptyExpression<T>
                ? expression 
                : Utility.Equals(exponent, 1d)
                    ? expression
                    : new ExponentExpression<T>(expression, exponent);
        
        public static InverseExpression<T> Invert<T>(this Expression<T> expression) where T : IExpressible
            => new InverseExpression<T>(expression);

        public static MultiplicationExpression<T> MultiplyWith<T>(this Expression<T> expression, Expression<T> other) where T : IExpressible
            => new MultiplicationExpression<T>(expression, other);

        public static MultiplicationSequenceExpression<T> MultiplyWith<T>(this Expression<T> expression, IEnumerable<Expression<T>> other) where T : IExpressible
            => new MultiplicationSequenceExpression<T>(new[] { expression }.Concat(other));

        public static PrefixExpression<T> Prefix<T>(this Expression<T> expression, Prefix prefix) where T : IExpressible
            => new PrefixExpression<T>(prefix, expression);

        public static ScaleExpression<T> Scale<T>(this Expression<T> expression, double scale) where T : IExpressible
            => new ScaleExpression<T>(scale, expression);

        public static Expression<T> ReplacePrefixes<T>(this Expression<T> expression, Prefix newPrefix) where T : IExpressible
        {
            var prExpr = expression as PrefixExpression<T>;
            if (prExpr != null)
                return prExpr.Content
                    .Transform(x => x.ReplacePrefixes(newPrefix)) // take care of nested instances (although that's crazy)
                    .Prefix(newPrefix).Scale(prExpr.Prefix.Value() / newPrefix.Value());
            return expression.Transform(x => x.ReplacePrefixes(newPrefix));
        }

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

        /// <summary>
        ///     Method to remove unnecessary parentheses in chained multiplications (a*(b*c)*d) -> (a*b*c*d)
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression<T> EliminateParentheses<T>(this Expression<T> expression) where T : IExpressible
            => ParenthesesTool.EliminateParentheses(expression.Transform(EliminateParentheses));

        /// <summary>
        ///     Simplify a given expression
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="options"></param>
        /// <returns></returns>
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