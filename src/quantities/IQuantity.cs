using Engineering.Expressions;

namespace Engineering.Quantities
{
    /// <summary>
    ///     Quantity interface
    /// </summary>
    public interface IQuantity : IExpressible
    {
        string Name { get; }
        string Symbol { get; }
    }
}