using Engineering.Units.Attributes;

namespace Engineering.Units
{
    public enum Prefix
    {
        [PrefixInformation(Factor = 1e18, Symbol = "E")]
        exa,
        [PrefixInformation(Factor = 1e15, Symbol = "P")]
        peta,
        [PrefixInformation(Factor = 1e12, Symbol = "T")]
        tera,
        [PrefixInformation(Factor = 1e9, Symbol = "G")]
        giga,
        [PrefixInformation(Factor = 1e6, Symbol = "M")]
        mega,
        [PrefixInformation(Factor = 1000, Symbol = "k")]
        kilo,
        [PrefixInformation(Factor = 100, Symbol = "h")]
        hecto,
        [PrefixInformation(Factor = 10, Symbol = "da")]
        deca,
        [PrefixInformation(Factor = 1.0, Symbol = "")]
        none,
        [PrefixInformation(Factor = 0.1, Symbol = "d")]
        deci,
        [PrefixInformation(Factor = 0.01, Symbol = "c")]
        centi,
        [PrefixInformation(Factor = 0.001, Symbol = "m")]
        milli,
        [PrefixInformation(Factor = 1e-6, Symbol = "µ")]
        micro,
        [PrefixInformation(Factor = 1e-9, Symbol = "n")]
        nano,
        [PrefixInformation(Factor = 1e-12, Symbol = "p")]
        pico,
        [PrefixInformation(Factor = 1e-15, Symbol = "f")]
        femto,
        [PrefixInformation(Factor = 1e-18, Symbol = "a")]
        atto
    }
}