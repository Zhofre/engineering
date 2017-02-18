using System;
using Engineering.Measurements;
using Engineering.Expressions;
using Engineering.Units;
using Engineering.Units.SI;
using static Engineering.Expressions.Prefix;

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
            var second = new Second();
            var m_s = new DerivedUnit("velocity", new DivisionExpression<IUnit>(meter, second));
            Console.WriteLine($"{m_s.Name}: {m_s.Notation} [{m_s.Quantity.Symbol}]");
            var m_s2 = new DerivedUnit("acceleration", new DivisionExpression<IUnit>(meter, new ExponentExpression<IUnit>(second, 2)));
            Console.WriteLine($"{m_s2.Name}: {m_s2.Notation} [{m_s2.Quantity.Symbol}]");
            var m_s2bis = new DerivedUnit("acceleration", new DivisionExpression<IUnit>(meter, new MultiplicationExpression<IUnit>(second, second)));
            Console.WriteLine($"{m_s2bis.Name}: {m_s2bis.Notation} [{m_s2bis.Quantity.Symbol}]");
            var kg = new DerivedUnit("kilogram", new PrefixExpression<IUnit>(kilo, new Gram()));
            var newton = new DerivedUnit("newton", "N", new MultiplicationExpression<IUnit>(kg, m_s2bis), "F");
            Console.WriteLine($"{newton.Name}: {newton.Notation}={newton.Expression.Representation} [{newton.Quantity.Symbol}]");
        }
    }
}
