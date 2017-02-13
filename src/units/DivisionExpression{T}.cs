using System;

namespace Engineering.Units
{
    public sealed class DivisionExpression<T> : BinaryExpression<T>
        where T : IEquatable<T>
    {
        public DivisionExpression(Expression<T> left, Expression<T> right) 
            : base (left, right)
        {
        }

        public override Expression<T> DivideBy(Expression<T> other)
        {
            throw new NotImplementedException();
        }

        public override Expression<T> MultiplyWith(Expression<T> other)
        {
            throw new NotImplementedException();
        }

        public override Expression<T> RaiseToPower(double exponent)
        {
            throw new NotImplementedException();
        }
    }
}