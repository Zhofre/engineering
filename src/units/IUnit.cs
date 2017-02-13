using System;

namespace Engineering.Units
{
    public interface IUnit : IEquatable<IUnit>
    {
        string Notation { get; }
        IQuantity Quantity { get; }
    }    
}
