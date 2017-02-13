using System;

namespace Engineering.Units
{
    public sealed class ConstantExpression<T> : Expression<T>
        where T : IEquatable<T>
    {
        public T Content { get; private set; }

        public ConstantExpression(T c)
        {
            Content = c;
        }

        public static implicit operator ConstantExpression<T>(T c)
            => new ConstantExpression<T>(c);

        public override Expression<T> MultiplyWith(Expression<T> other)
        {
            throw new NotImplementedException();
        }

        public override Expression<T> DivideBy(Expression<T> other)
        {
            throw new NotImplementedException();
        }

        public override Expression<T> RaiseToPower(double exponent)
            => new ExponentExpression<T>(this, exponent);
    }
}