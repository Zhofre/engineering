namespace Engineering.Units
{
    /// <summary>
    ///     Quantity interface
    /// </summary>
    public interface IQuantity
    {
        string Name { get; }
        string Symbol { get; }
        IUnit SIUnit { get; }
    }
}