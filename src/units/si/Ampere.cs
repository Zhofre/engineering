using Engineering.Quantities;

namespace Engineering.Units.SI
{
    public sealed class Ampere : BaseUnit
    {
        public Ampere() : base("ampere", "A", new ElectricCurrent())
        {
        }
    }
}