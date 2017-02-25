using Engineering.Expressions;
using Engineering.Quantities;
using Engineering.Units;
using Engineering.Units.SI;
using Xunit;

namespace Engineering.Tests
{
    public class ExpressionTests
    {
        [Fact]
        public void EmptyExpressionRepresentation()
        {
            // arrange
            var sut = new EmptyExpression<IQuantity>();

            // act
            var result = sut.Representation;

            // assert
            Assert.Equal("-", result);
        }

        [Fact]
        public void ConstantExpressionRepresentation()
        {
            // arrange
            var l = new Length();
            var sut = new ConstantExpression<IQuantity>(l);

            // act
            var result = sut.Representation;

            // assert
            Assert.Equal("L", result);
        }

        [Fact]
        public void PrefixExpressionQuantityRepresentation()
        {
            // arrange
            var l = new Length();
            var c = new ConstantExpression<IQuantity>(l);
            var sut = new PrefixExpression<IQuantity>(Prefix.kilo, c);

            // act
            var result = sut.Representation;

            // assert
            Assert.Equal("L", result);
        }

        [Fact]
        public void PrefixExpressionUnitRepresentation()
        {
            // arrange
            var m = new Meter();
            var c = new ConstantExpression<IUnit>(m);
            var sut = new PrefixExpression<IUnit>(Prefix.kilo, c);

            // act
            var result = sut.Representation;

            // assert
            Assert.Equal("km", result);
        }


    }
}