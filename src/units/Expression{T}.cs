using System;

namespace Engineering.Units
{
    /// <summary>
    ///     Expressions
    /// </summary>
    public abstract class Expression<T>
        where T : IEquatable<T>
    {
        public abstract Expression<T> MultiplyWith(Expression<T> other);
        public abstract Expression<T> DivideBy(Expression<T> other);
        public abstract Expression<T> RaiseToPower(double exponent);

        public static Expression<T> operator *(Expression<T> left, Expression<T> right)
            => left.MultiplyWith(right);
        public static Expression<T> operator /(Expression<T> left, Expression<T> right)
            => left.DivideBy(right);
    }
}