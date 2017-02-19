using System.Collections.Generic;

namespace Engineering.Expressions.Fluent
{
    /// <summary>
    ///     Helper class for the deconstruction of complex expressions
    /// </summary>
    internal class ClassifiedExpression<T>
        where T : IExpressible
    {
        public ClassifiedExpression(Expression<T> expression, bool expandPrefix)
        {
            if (expression is PrefixExpression<T> || !expandPrefix)
            {
                Numerator.Add(expression);
                return;
            }

            var classifiedExpression = expression as IClassifiable<T>;
            Exponent = classifiedExpression.GetExponent();
            Scale = classifiedExpression.GetScale();
            var n = classifiedExpression.GetNumerator();
            if (n != null)
                Numerator.AddRange(n);
            var d = classifiedExpression.GetDenominator();
            if (d != null)
                Denominator.AddRange(d);
        }

        public double Exponent { get; set; } = 1d;
        public double Scale { get; set; } = 1d;
        public List<Expression<T>> Numerator { get; } = new List<Expression<T>>();
        public List<Expression<T>> Denominator { get; } = new List<Expression<T>>();
    }
}