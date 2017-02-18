namespace Engineering.Expressions
{    
    public interface IExpressible
    {
        bool CanScale { get; }
        string Representation { get; }
    }   
}