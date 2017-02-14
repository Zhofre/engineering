using System;
using Engineering.Measurements;
using Engineering.Units;
using Engineering.Units.Expressions;
using Engineering.Units.SI;
using static Engineering.Units.Prefix;

namespace TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Units demo code");
            // todo: provide demo code
            var t = 5.0.Meters();
            Console.WriteLine($"Extension test: {t}");
            var meter = new Meter();
            var km = new DerivedUnit("kilometer", kilo*meter);
            Console.WriteLine($"{km.Name}: {km.Notation} [{km.Quantity.Symbol}]");
            var m3 = new DerivedUnit("cubic meters", new ExponentExpression<IUnit>(meter, 3));
            Console.WriteLine($"{m3.Name}: {m3.Notation} [{m3.Quantity.Symbol}]");
            var km2 = new DerivedUnit("squared kilometers", new ExponentExpression<IUnit>(km, 2));
            Console.WriteLine($"{km2.Name}: {km2.Notation} [{km2.Quantity.Symbol}]");
        }
    }
}
