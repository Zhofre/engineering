using Engineering.Units.Expressions;

namespace Engineering.Units
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