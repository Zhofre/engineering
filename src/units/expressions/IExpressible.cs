namespace Engineering.Units.Expressions
{    
    public interface IExpressible
    {
        bool CanPrefix { get; }
        string Representation { get; }
    }   
}