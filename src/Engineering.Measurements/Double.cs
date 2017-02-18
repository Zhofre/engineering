using Engineering.Units;

namespace Engineering.Measurements
{
    public sealed class Double : Measurement<double>
    {
        public Double(double value, IUnit unit)
            : base(value, unit)
        {
        }
    }
}