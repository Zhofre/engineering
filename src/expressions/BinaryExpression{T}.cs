namespace Engineering.Expressions
{
    public abstract class BinaryExpression<T> : Expression<T>
        where T : IExpressible
    {
        public BinaryExpression(Expression<T> lhs, Expression<T> rhs)
        {
            LeftHandSide = lhs;
            RightHandSide = rhs;
        }

        public Expression<T> LeftHandSide { get; }

        public Expression<T> RightHandSide { get; }

        public override bool CanScale => LeftHandSide.CanScale && RightHandSide.CanScale;

        public override string Representation
            => LeftHandSide.AutoBracketedRepresentation 
                + OperatorSymbol 
                + RightHandSide.AutoBracketedRepresentation;

        protected abstract string OperatorSymbol { get; }
    }
}