using Engineering.Expressions;
using Engineering.Quantities;

namespace Engineering.Units
{
    public interface IUnit : IExpressible
    {
        string Name { get; }
        string Notation { get; }
        IQuantity Quantity { get; }
    }    
}
