using System;
using Engineering.Expressions;
using Engineering.Quantities;

namespace Engineering.Units
{
    public interface IUnit : IExpressible, IEquatable<IUnit>
    {
        string Name { get; }
        string Notation { get; }
        IQuantity Quantity { get; }
    }    
}
