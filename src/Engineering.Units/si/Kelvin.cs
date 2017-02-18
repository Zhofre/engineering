using Engineering.Quantities;

namespace Engineering.Units.SI
{
    public sealed class Kelvin : BaseUnit
    {
        public Kelvin() : base("kelvin", "K", new Temperature())
        {
        }
    }
}