using Engineering.Units;

namespace Engineering.Measurements
{
    public abstract class Measurement<T>
    {
        protected Measurement(T value, IUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        public T Value { get; }
        public IUnit Unit { get; }

        public override string ToString()
        {
            return Value + " " + Unit.Notation;
        }
    }
}
