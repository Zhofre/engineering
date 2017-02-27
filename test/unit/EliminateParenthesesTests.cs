using Engineering.Expressions;
using Engineering.Expressions.Fluent;
using Engineering.Quantities;
using Xunit;

namespace Engineering.Tests
{
    public class EliminateParenthesesTests
    {
        [Fact]
        public void SingleMultiplicationElimination()
        {
            var l = new Length();
            var t = new Time();
            var tmp = new MultiplicationExpression<IQuantity>(l, t);
            var sut = new MultiplicationExpression<IQuantity>(t, tmp);

            var result = sut.EliminateParentheses();

            Assert.Equal("T*(L*T)", sut.Representation);
            Assert.Equal("T*L*T", result.Representation);
        }

        [Fact]
        public void DoubleMultiplicationElimination()
        {
            var l = new Length();
            var t = new Time();
            var tmp = new MultiplicationExpression<IQuantity>(l, t);
            var tmp2 = new MultiplicationExpression<IQuantity>(t, tmp);
            var sut = new MultiplicationExpression<IQuantity>(tmp2, l);

            var result = sut.EliminateParentheses();

            Assert.Equal("(T*(L*T))*L", sut.Representation);
            Assert.Equal("T*L*T*L", result.Representation);
        }

        [Fact]
        public void SingleMultiplicationSequenceElimination()
        {
            var l = new Length();
            var t = new Time();
            var tmp = new MultiplicationSequenceExpression<IQuantity>(l, t, l);
            var sut = new MultiplicationSequenceExpression<IQuantity>(tmp, t, tmp);

            var result = sut.EliminateParentheses();

            Assert.Equal("(L*T*L)*T*(L*T*L)", sut.Representation);
            Assert.Equal("L*T*L*T*L*T*L", result.Representation);
        }

        [Fact]
        public void DoubleMultiplicationSequenceElimination()
        {
            var l = new Length();
            var t = new Time();
            var tmp = new MultiplicationSequenceExpression<IQuantity>(l, t, l);
            var tmp2 = new MultiplicationSequenceExpression<IQuantity>(tmp, t, tmp);
            var sut = new MultiplicationSequenceExpression<IQuantity>(tmp2, l, tmp2);

            var result = sut.EliminateParentheses();

            Assert.Equal("((L*T*L)*T*(L*T*L))*L*((L*T*L)*T*(L*T*L))", sut.Representation);
            Assert.Equal("L*T*L*T*L*T*L*L*L*T*L*T*L*T*L", result.Representation);
        }

        [Fact]
        public void DoubleMultiplicationMixedElimination()
        {
            var l = new Length();
            var t = new Time();
            var tmp = new MultiplicationSequenceExpression<IQuantity>(l, t, l);
            var tmp2 = new MultiplicationExpression<IQuantity>(tmp, t);
            var sut = new MultiplicationSequenceExpression<IQuantity>(tmp2, l, tmp2);

            var result = sut.EliminateParentheses();

            Assert.Equal("((L*T*L)*T)*L*((L*T*L)*T)", sut.Representation);
            Assert.Equal("L*T*L*T*L*L*T*L*T", result.Representation);
        }

        [Fact]
        public void DivisionPartsMultiplicationElimination()
        {
            var l = new Length();
            var t = new Time();
            var tmp = new MultiplicationExpression<IQuantity>(l, t);
            var tmp2 = new MultiplicationExpression<IQuantity>(t, tmp);
            var sut = new DivisionExpression<IQuantity>(tmp2, tmp2);

            var result = sut.EliminateParentheses();

            Assert.Equal("(T*(L*T))/(T*(L*T))", sut.Representation);
            Assert.Equal("(T*L*T)/(T*L*T)", result.Representation);
        }


    }
}