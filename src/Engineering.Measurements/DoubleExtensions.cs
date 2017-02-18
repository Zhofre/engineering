using Engineering.Units.SI;

namespace Engineering.Measurements
{
    public static class DoubleExtensions
    {
        public static Double Meters(this double value)
            => new Double(value, new Meter());

        public static Double Seconds(this double value)
            => new Double(value, new Second());

        public static Double Grams(this double value)
            => new Double(value, new Gram());

        public static Double Kelvin(this double value)
            => new Double(value, new Kelvin());
    }
}