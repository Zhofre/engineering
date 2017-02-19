using System;
using System.Collections.Generic;
using System.Linq;

namespace Engineering.Expressions.Fluent
{
    internal static class ExpandTool
    {
        public static Expression<T> Aggresive<T>(Expression<T> expression, bool expandPrefix = false)
            where T : IExpressible
        {
            //var scale = 1d;
            //var items = new List<Expression<T>>();

            throw new System.NotImplementedException();
        }

        public static Expression<T> Normal<T>(Expression<T> expression, bool expandPrefix = false)
            where T : IExpressible
        {
            var cExpr = expression.Convert(expandPrefix, true);


            var numer = new MultiplicationSequenceExpression<T>(cExpr.Numerator);
            var denom = new MultiplicationSequenceExpression<T>(cExpr.Denominator);
            
            // todo: autoexponent might cause infinity recursion

            throw new System.NotImplementedException();
            
        }

        internal static Expression<T> AutoExponent<T>(this Expression<T> expression, double exponent)
            where T : IExpressible
            => Utility.Equals(exponent, 1d)
                ? expression
                : new ExponentExpression<T>(expression, exponent);

        internal static ClassifiedExpression<T> Convert<T>(this Expression<T> expression, bool expandPrefix, bool recursive)
            where T : IExpressible
        {
            var ce = new ClassifiedExpression<T>(expression, expandPrefix);
            if (!recursive)
                return ce;

            // recursion halting
            if (expression is ConstantExpression<T>
                || expression is PrefixExpression<T>)
                return ce;

            // recursively evaluate numerator/denominator
            var numeratorStack = new List<Expression<T>>();
            var denominatorStack = new List<Expression<T>>();
            numeratorStack.AddRange(ce.Numerator);
            denominatorStack.AddRange(ce.Denominator);
            ce.Numerator.Clear();
            ce.Denominator.Clear();

            foreach(var x in numeratorStack)
            {
                var ce2 = x.Convert(expandPrefix, recursive);
                ce.Scale *= ce2.Scale;
                ce.Numerator.AddRange(ce2.Numerator.Select(e => e.AutoExponent(ce2.Exponent)));
                ce.Denominator.AddRange(ce2.Denominator.Select(e => e.AutoExponent(ce2.Exponent)));
            }
            foreach(var x in denominatorStack)
            {
                var ce2 = x.Convert(expandPrefix, recursive);
                ce.Scale /= ce2.Scale;
                ce.Numerator.AddRange(ce2.Denominator.Select(e => e.AutoExponent(ce2.Exponent)));
                ce.Denominator.AddRange(ce2.Numerator.Select(e => e.AutoExponent(ce2.Exponent)));
            }
            return ce;
        }
    }
}