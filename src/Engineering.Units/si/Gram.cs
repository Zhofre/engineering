using Engineering.Quantities;

namespace Engineering.Units.SI
{
    public sealed class Gram : BaseUnit
    {
        public Gram() : base("gram", "g", new Mass())
        {
        }
    }
}