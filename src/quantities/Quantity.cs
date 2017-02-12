using System;
using Engineering.Units;

namespace Engineering.Quantities
{
    public class Quantity
    {
        public double Value { get; set; }
        public IUnit Unit { get; set; }
    }
}
