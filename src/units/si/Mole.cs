using Engineering.Quantities;

namespace Engineering.Units.SI
{
    public sealed class Mole : BaseUnit
    {
        public Mole() : base("mole", "mol", new AmountOfSubstance())
        {
        }
    }
}