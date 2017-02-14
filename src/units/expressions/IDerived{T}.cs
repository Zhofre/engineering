namespace Engineering.Units.Expressions
{
    public interface IDerived<T> where T : IExpressible
    {
        Expression<T> Expression { get; }
    }
}