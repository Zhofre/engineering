namespace Engineering.Expressions
{
    public abstract class UnaryExpression<T> : Expression<T>
        where T : IExpressible
    {
        protected UnaryExpression(Expression<T> expression)
        {
            Content = expression;
        }

        public Expression<T> Content { get; }

        public override bool CanScale => Content.CanScale;
    }
}