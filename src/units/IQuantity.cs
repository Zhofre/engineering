using System;

namespace Engineering.Units
{
    /// <summary>
    ///     Quantity interface
    /// </summary>
    public interface IQuantity : IEquatable<IQuantity>
    {
        string Name { get; }
        string Symbol { get; }
        IUnit BaseUnit { get; }
    }
}