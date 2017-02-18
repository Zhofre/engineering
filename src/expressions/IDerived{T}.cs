namespace Engineering.Expressions
{
    public interface IDerived<T> where T : IExpressible
    {
        Expression<T> Expression { get; }
    }
}