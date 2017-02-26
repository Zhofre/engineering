using Engineering.Expressions;
using Engineering.Quantities;
using Engineering.Units;
using Engineering.Units.SI;
using Xunit;

namespace Engineering.Tests
{
    /// <summary>
    ///     Class to test the construction of expressions
    /// </summary>
    public class ExpressionTests
    {
        [Fact]
        public void EmptyExpressionRepresentation()
        {
            var sut = new EmptyExpression<IQuantity>();

            var result = sut.Representation;

            Assert.Equal("-", result);
        }

        [Fact]
        public void ConstantExpressionRepresentation()
        {
            var l = new Length();
            var sut = new ConstantExpression<IQuantity>(l);

            var result = sut.Representation;

            Assert.Equal("L", result);
        }

        [Fact]
        public void PrefixExpressionQuantityRepresentation()
        {
            var l = new Length();
            var c = new ConstantExpression<IQuantity>(l);
            var sut = new PrefixExpression<IQuantity>(Prefix.kilo, c);

            var result = sut.Representation;

            Assert.Equal("L", result);
        }

        [Fact]
        public void PrefixExpressionUnitRepresentation1()
        {
            var m = new Meter();
            var c = new ConstantExpression<IUnit>(m);
            var sut = new PrefixExpression<IUnit>(Prefix.kilo, c);

            var result = sut.Representation;

            Assert.Equal("km", result);
        }

        [Fact]
        public void PrefixExpressionUnitRepresentation2()
        {
            var m = new Meter();
            var c = new ConstantExpression<IUnit>(m);
            var sut = new PrefixExpression<IUnit>(Prefix.none, c);

            var result = sut.Representation;

            Assert.Equal("m", result);
        }

        [Fact]
        public void ImplicitConstantExpression()
        {
            var sut = new Length();

            Expression<IQuantity> result = sut;

            Assert.True(result is ConstantExpression<IQuantity>);
        }

        [Fact]
        public void ScaleExpressionQuantityRepresentation()
        {
            var l = new Length();
            var sut = new ScaleExpression<IQuantity>(0.5, l);

            var result = sut.Representation;

            Assert.Equal("L", result);
        }

        [Fact]
        public void ScaleExpressionUnitRepresentation1()
        {
            var m = new Meter();
            var sut = new ScaleExpression<IUnit>(0.5, m);

            var result = sut.Representation;

            Assert.Equal("0.5*m", result);
        }

        [Fact]
        public void ScaleExpressionUnitRepresentation2()
        {
            var m = new Meter();
            var sut = new ScaleExpression<IUnit>(1.0, m);

            var result = sut.Representation;

            Assert.Equal("m", result);
        }

        [Fact]
        public void ExponentExpressionRepresentation1()
        {
            var l = new Length();
            var sut = new ExponentExpression<IQuantity>(l, 2);

            var result = sut.Representation;

            Assert.Equal("L^2", result);
        }

        [Fact]
        public void ExponentExpressionRepresentation2()
        {
            var l = new Length();
            var sut = new ExponentExpression<IQuantity>(l, 1);

            var result = sut.Representation;

            Assert.Equal("L", result);
        }

        [Fact]
        public void MultiplicationExpressionRespresentation()
        {
            var l = new Length();
            var t = new Time();
            var sut = new MultiplicationExpression<IQuantity>(l, t);

            var result = sut.Representation;

            Assert.Equal("L*T", result);
        }

        [Fact]
        public void DivisionExpressionRepresentation()
        {
            var l = new Length();
            var t = new Time();
            var sut = new DivisionExpression<IQuantity>(l, t);

            var result = sut.Representation;

            Assert.Equal("L/T", result);
        }

        [Fact]
        public void MultiplicationSequenceExpressionRepresentation()
        {
            var l = new Length();
            var t = new Time();
            var sut = new MultiplicationSequenceExpression<IQuantity>(l, t, t, l);

            var result = sut.Representation;

            Assert.Equal("L*T*T*L", result);
        }

        [Fact]
        public void InverseExpressionRepresentation()
        {
            var t = new Time();
            var sut = new InverseExpression<IQuantity>(t);

            var result = sut.Representation;

            Assert.Equal("1/T", result);
        }

        [Fact]
        public void ComplexExpressionRepresentation1()
        {
            var l = new Length();
            var t = new Time();
            var expr1 = new DivisionExpression<IQuantity>(l, t);
            var sut = new MultiplicationExpression<IQuantity>(t, expr1);

            var result = sut.Representation;

            Assert.Equal("T*(L/T)", result);
        }

        [Fact]
        public void ComplexExpressionRepresentation2()
        {
            var l = new Length();
            var t = new Time();
            var expr1 = new ExponentExpression<IQuantity>(t, 2);
            var sut = new DivisionExpression<IQuantity>(l, expr1);

            var result = sut.Representation;

            Assert.Equal("L/T^2", result);
        }

    }
}