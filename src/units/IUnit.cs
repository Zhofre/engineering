namespace Engineering.Units
{
    public interface IUnit
    {
        string Notation { get; }
        IQuantity Quantity { get; }
    }    
}
