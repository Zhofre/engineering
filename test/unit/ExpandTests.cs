using Engineering.Expressions;
using Engineering.Units;
using Engineering.Units.SI;
using Xunit;
using static Engineering.Expressions.Prefix;
using static Engineering.Expressions.Fluent.ExpandOptions;
using Engineering.Expressions.Fluent;

namespace Engineering.Tests
{
    public class ExpandTests
    {
        [Fact]
        public void Expand1()
        {
            var meter = new Meter();
            var second = new Second();
            var kg = new DerivedUnit("kilogram", new PrefixExpression<IUnit>(kilo, new Gram()));
            var m_s2 = new DerivedUnit("acceleration", new DivisionExpression<IUnit>(meter, new ExponentExpression<IUnit>(second, 2)));
            var newton = new DerivedUnit("newton", "N", new MultiplicationExpression<IUnit>(kg, m_s2), "F");
            var millimeter = new DerivedUnit("millimeter", milli * meter);
            var sut = new ExponentExpression<IUnit>(millimeter, 2d);

            var normalExpansion = sut.Expand();
            var normalPrefixExpansion = sut.Expand(PrefixToScale);
            var aggressiveExpansion = sut.Expand(Aggressive);
            var aggressivePrefixExpansion = sut.Expand(Aggressive | PrefixToScale);

            Assert.Equal("mm^2", normalExpansion.Representation);
            Assert.Equal("1E-06*m^2", normalPrefixExpansion.Representation);
            Assert.Equal("mm^2", aggressiveExpansion.Representation);
            Assert.Equal("1E-06*m^2", aggressivePrefixExpansion.Representation);
        }

        [Fact]
        public void Expand2()
        {
            var meter = new Meter();
            var second = new Second();
            var kg = new DerivedUnit("kilogram", new PrefixExpression<IUnit>(kilo, new Gram()));
            var m_s2 = new DerivedUnit("acceleration", new DivisionExpression<IUnit>(meter, new ExponentExpression<IUnit>(second, 2)));
            var newton = new DerivedUnit("newton", "N", new MultiplicationExpression<IUnit>(kg, m_s2), "F");
            var millimeter = new DerivedUnit("millimeter", milli * meter);
            var millimeter2 = new ExponentExpression<IUnit>(millimeter, 2d);
            var sut = new DerivedUnit("megapascals", "MPa", new DivisionExpression<IUnit>(newton, millimeter2));

            var normalExpansion = sut.Expression.Expand();
            var normalPrefixExpansion = sut.Expression.Expand(PrefixToScale);
            var aggressiveExpansion = sut.Expression.Expand(Aggressive);
            var aggressivePrefixExpansion = sut.Expression.Expand(Aggressive | PrefixToScale);

            Assert.Equal("(kg*m)/(s^2*mm^2)", normalExpansion.Representation);
            Assert.Equal("1000000000*((g*m)/(s^2*m^2))", normalPrefixExpansion.Representation);
            Assert.Equal("kg*m*s^-2*mm^-2", aggressiveExpansion.Representation);
            Assert.Equal("1000000000*(g*m*s^-2*m^-2)", aggressivePrefixExpansion.Representation);
        }

    }
}