using System;
using Engineering.Expressions;

namespace Engineering.Quantities
{
    /// <summary>
    ///     Quantity interface
    /// </summary>
    public interface IQuantity : IExpressible, IEquatable<IQuantity>
    {
        string Name { get; }
        string Symbol { get; }
    }
}