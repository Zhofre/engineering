using System;
using System.Collections.Generic;
using System.Linq;

namespace Engineering.Expressions.Fluent
{
    internal static class ExpandTool
    {
        public static Expression<T> Aggressive<T>(Expression<T> expression, bool expandPrefix = false)
            where T : IExpressible
        {
            var cExpr = expression.Convert(expandPrefix, true);
            // invert the denominator
            var numer = new List<Expression<T>>(cExpr.Numerator);
            foreach (var x in cExpr.Denominator)
            {
                var expExpression = x as ExponentExpression<T>;
                if (expExpression == null)
                    numer.Add(new ExponentExpression<T>(x, -1d));

                var modifiedExpression = new ExponentExpression<T>(expExpression.Content, -expExpression.Exponent);
                numer.Add(modifiedExpression);
            }
            return BuildExpression(numer, null, cExpr.Exponent).AutoScale(cExpr.Scale, cExpr.Exponent);
        }

        public static Expression<T> Normal<T>(Expression<T> expression, bool expandPrefix = false)
            where T : IExpressible
        {
            var cExpr = expression.Convert(expandPrefix, true);
            return BuildExpression(cExpr.Numerator, cExpr.Denominator, cExpr.Exponent).AutoScale(cExpr.Scale, cExpr.Exponent);
        }

        private static Expression<T> BuildExpression<T>(List<Expression<T>> factors, double exponent)
            where T : IExpressible
        {
            var fac = factors ?? new List<Expression<T>>();
            switch (fac.Count)
            {
                case 0 : return null;
                case 1 : return fac[0].AutoExponent(exponent);
                case 2 : return new MultiplicationExpression<T>(fac[0].AutoExponent(exponent), fac[1].AutoExponent(exponent));
                default : return new MultiplicationSequenceExpression<T>(fac.Select(x => x.AutoExponent(exponent)));
            }
        }

        private static Expression<T> BuildExpression<T>(List<Expression<T>> numerator, List<Expression<T>> denominator, double exponent)
            where T : IExpressible
        {
            var num = BuildExpression(numerator, exponent);
            var den = BuildExpression(denominator, exponent);

            if (num == null)
                return new InverseExpression<T>(den);
            if (den == null)
                return num;            
            return new DivisionExpression<T>(num, den);
        }

        private static Expression<T> AutoScale<T>(this Expression<T> expression, double scale, double exponent) where T : IExpressible
            => Utility.Equals(scale, 1d)
                ? expression
                : new ScaleExpression<T>(Math.Pow(scale, exponent), expression);

        private static Expression<T> AutoExponent<T>(this Expression<T> expression, double exponent) where T : IExpressible
            => Utility.Equals(exponent, 1d)
                ? expression
                : new ExponentExpression<T>(expression, exponent);

        private static bool IsBaseExpression<T>(this Expression<T> expression, bool expandPrefix) where T : IExpressible
            => expression is ConstantExpression<T> || (!expandPrefix && expression is PrefixExpression<T>);

        internal static ClassifiedExpression<T> Convert<T>(this Expression<T> expression, bool expandPrefix, bool recursive)
            where T : IExpressible
        {
            var ce = new ClassifiedExpression<T>(expression, expandPrefix);
            if (!recursive)
                return ce;

            // recursion halting
            if (expression.IsBaseExpression(expandPrefix))
                return ce;
            var expExpression = expression as ExponentExpression<T>;
            if (expExpression != null && expExpression.Content.IsBaseExpression(expandPrefix))
                return ce;

            // recursively evaluate numerator/denominator
            var numeratorStack = new List<Expression<T>>();
            var denominatorStack = new List<Expression<T>>();
            numeratorStack.AddRange(ce.Numerator);
            denominatorStack.AddRange(ce.Denominator);
            ce.Numerator.Clear();
            ce.Denominator.Clear();

            foreach (var x in numeratorStack)
            {
                var ce2 = x.Convert(expandPrefix, recursive);
                ce.Scale *= Math.Pow(ce2.Scale, ce2.Exponent);
                ce.Numerator.AddRange(ce2.Numerator.Select(e => e.AutoExponent(ce2.Exponent)));
                ce.Denominator.AddRange(ce2.Denominator.Select(e => e.AutoExponent(ce2.Exponent)));
            }
            foreach (var x in denominatorStack)
            {
                var ce2 = x.Convert(expandPrefix, recursive);
                ce.Scale /= Math.Pow(ce2.Scale, ce2.Exponent);
                ce.Numerator.AddRange(ce2.Denominator.Select(e => e.AutoExponent(ce2.Exponent)));
                ce.Denominator.AddRange(ce2.Numerator.Select(e => e.AutoExponent(ce2.Exponent)));
            }
            return ce;
        }
    }
}