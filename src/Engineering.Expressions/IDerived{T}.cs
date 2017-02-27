namespace Engineering.Expressions
{
    public interface IDerived<T> : IExpressible
        where T : IExpressible
    {
        Expression<T> Expression { get; }
        bool HasCustomRepresentation { get; }
    }
}