using System;

namespace Engineering.Expressions
{    
    public interface IExpressible : IEquatable<IExpressible>
    {
        bool CanScale { get; }
        string Representation { get; }
    }   
}