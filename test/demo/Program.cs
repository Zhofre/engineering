using System;
using Engineering.Measurements;
using Engineering.Expressions;
using Engineering.Expressions.Fluent;
using Engineering.Units;
using Engineering.Units.SI;
using static Engineering.Expressions.Prefix;
using static Engineering.Expressions.Fluent.ExpandOptions;

namespace TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Units demo code");
            
            DerivedUnits();
            ExpressionExpanding();
            // todo: provide demo code
            
            
        }

        private static void DerivedUnits()
        {
            Console.WriteLine("Deriving units demo:");
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
            var sinv = new ExponentExpression<IUnit>(second, -1);
            var newton2 = new DerivedUnit("newton", "N", new MultiplicationSequenceExpression<IUnit>(kg, meter, sinv, sinv), "F");
            Console.WriteLine($"{newton2.Name}: {newton2.Notation}={newton2.Expression.Representation} [{newton2.Quantity.Symbol}]");
        }

        private static void ExpressionExpanding()
        {
            Console.WriteLine("Expression expand demo:");
            var meter = new Meter();
            var second = new Second();
            var kg = new DerivedUnit("kilogram", new PrefixExpression<IUnit>(kilo, new Gram()));
            var m_s2 = new DerivedUnit("acceleration", new DivisionExpression<IUnit>(meter, new ExponentExpression<IUnit>(second, 2)));
            var newton = new DerivedUnit("newton", "N", new MultiplicationExpression<IUnit>(kg, m_s2), "F");
            Console.WriteLine($"{newton.Name}: {newton.Notation}={newton.Expression.Representation} [{newton.Quantity.Symbol}]");
            var millimeter = new DerivedUnit("millimeter", milli * meter);
            var millimeter2 = new ExponentExpression<IUnit>(millimeter, 2d);
            var megapascals = new DerivedUnit("megapascals", "MPa", new DivisionExpression<IUnit>(newton, millimeter2));
            Console.WriteLine($"{megapascals.Name}: {megapascals.Notation}={megapascals.Expression.Representation} [{megapascals.Quantity.Symbol}]");

            Console.WriteLine($"Original {millimeter2.Representation}");
            var normalExpansion1 = millimeter2.Expand();
            var normalPrefixExpansion1 = millimeter2.Expand(PrefixToScale);
            var aggressiveExpansion1 = millimeter2.Expand(Aggressive);
            var aggressivePrefixExpansion1 = millimeter2.Expand(Aggressive | PrefixToScale);
            Console.WriteLine($"Normal: {normalExpansion1.Representation} == {normalPrefixExpansion1.Representation}");
            Console.WriteLine($"Aggressive: {aggressiveExpansion1.Representation} == {aggressivePrefixExpansion1.Representation}");            

            Console.WriteLine($"Original {megapascals.Expression.Representation}");
            var normalExpansion2 = megapascals.Expression.Expand();
            var normalPrefixExpansion2 = megapascals.Expression.Expand(PrefixToScale);
            var aggressiveExpansion2 = megapascals.Expression.Expand(Aggressive);
            var aggressivePrefixExpansion2 = megapascals.Expression.Expand(Aggressive | PrefixToScale);
            Console.WriteLine($"Normal: {normalExpansion2.Representation} == {normalPrefixExpansion2.Representation}");
            Console.WriteLine($"Aggressive: {aggressiveExpansion2.Representation} == {aggressivePrefixExpansion2.Representation}");            
        }
        
    }
}
