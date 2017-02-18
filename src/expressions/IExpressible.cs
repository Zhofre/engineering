namespace Engineering.Expressions
{    
    public interface IExpressible
    {
        bool CanPrefix { get; }
        string Representation { get; }
    }   
}