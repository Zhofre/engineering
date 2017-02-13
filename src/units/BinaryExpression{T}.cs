using System;

namespace Engineering.Units
{
    public abstract class BinaryExpression<T> : Expression<T>
        where T : IEquatable<T>
    {
        public Expression<T> LeftOperand { get; private set; }
        public Expression<T> RightOperand { get; private set; }
        public BinaryExpression(Expression<T> left, Expression<T> right)
        {
            LeftOperand = left;
            RightOperand = right;
        }
    }
}