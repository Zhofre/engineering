using System;

namespace Engineering.Units
{
    public sealed class ExponentExpression<T> : Expression<T>
        where T : IEquatable<T>
    {
        public double Exponent { get; private set; }

        public Expression<T> Content { get; private set; }

        public ExponentExpression(Expression<T> e, double exp = 1.0)
        {
            Content = e;
            Exponent = exp;
        }

        public override Expression<T> MultiplyWith(Expression<T> other)
        {
            throw new NotImplementedException();
        }

        public override Expression<T> DivideBy(Expression<T> other)
        {
            throw new NotImplementedException();
        }

        public override Expression<T> RaiseToPower(double exponent)
        {
            throw new NotImplementedException();
        }
    }
}