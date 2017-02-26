using Engineering.Expressions;
using Engineering.Quantities;
using Engineering.Units;
using Engineering.Units.SI;
using Xunit;
using static Engineering.Expressions.Prefix;

namespace Engineering.Tests
{
    public class DerivedUnitTests
    {
        [Fact]
        public void DerivedUnitPrefixTest()
        {
            var meter = new Meter();
            var sut = new DerivedUnit("kilometer", kilo * meter);

            var result1 = sut.Name;
            var result2 = sut.Notation;
            var result3 = sut.Quantity.Symbol;

            Assert.Equal("kilometer", result1);
            Assert.Equal("km", result2);
            Assert.Equal("L", result3);
        }

        [Fact]
        public void DerivedUnitExponentTest()
        {
            var meter = new Meter();
            var sut = new DerivedUnit("cubic meters", new ExponentExpression<IUnit>(meter, 3));

            var result1 = sut.Notation;
            var result2 = sut.Quantity.Symbol;

            Assert.Equal("m^3", result1);
            Assert.Equal("L^3", result2);
        }

        [Fact]
        public void DerivedUnitComplexTest1()
        {
            var meter = new Meter();
            var second = new Second();
            var meter_second2 = new DerivedUnit("acceleration", new DivisionExpression<IUnit>(meter, new ExponentExpression<IUnit>(second, 2)));
            var kg = new DerivedUnit("kilogram", new PrefixExpression<IUnit>(kilo, new Gram()));
            var sut = new DerivedUnit("newton", "N", new MultiplicationExpression<IUnit>(kg, meter_second2), "F");
            var sut2 = sut.Quantity as IDerived<IQuantity>;

            var result1 = sut.Notation;
            var result2 = sut.Expression.Representation;
            var result3 = sut.Quantity.Symbol;
            var result4 = sut2.Expression.Representation;

            Assert.Equal("N", result1);
            Assert.Equal("kg*(m/s^2)", result2);
            Assert.Equal("F", result3);
            Assert.Equal("M*(L/T^2)", result4);
        }

        [Fact]
        public void DerivedUnitComplexTest2()
        {
            var meter = new Meter();
            var second = new Second();
            var sinv = new ExponentExpression<IUnit>(second, -1);
            var kg = new DerivedUnit("kilogram", new PrefixExpression<IUnit>(kilo, new Gram()));
            var sut = new DerivedUnit("newton", "N", new MultiplicationSequenceExpression<IUnit>(kg, meter, sinv, sinv), "F");
            var sut2 = sut.Quantity as IDerived<IQuantity>;

            var result1 = sut.Notation;
            var result2 = sut.Expression.Representation;
            var result3 = sut.Quantity.Symbol;
            var result4 = sut2.Expression.Representation;

            Assert.Equal("N", result1);
            Assert.Equal("kg*m*s^-1*s^-1", result2);
            Assert.Equal("F", result3);
            Assert.Equal("M*L*T^-1*T^-1", result4);
        }
    }
}
