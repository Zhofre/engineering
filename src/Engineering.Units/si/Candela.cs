using Engineering.Quantities;

namespace Engineering.Units.SI
{
    public sealed class Candela : BaseUnit
    {
        public Candela() : base("candela", "cd", new LuminousIntensity())
        {
        }
    }
}