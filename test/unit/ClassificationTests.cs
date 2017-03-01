using Engineering.Expressions;
using Engineering.Quantities;
using Engineering.Expressions.Fluent;
using Xunit;
using System.Collections.Generic;

namespace Engineering.Tests
{
    public class ClassificationTests
    {
        public static class ConstantExpressionDataSource
        {
            private static readonly ConstantExpression<IQuantity> _ce
                = new ConstantExpression<IQuantity>(new Length());
            private static readonly List<object[]> _data 
                = new List<object[]>
                {
                    new object[] { _ce, true },
                    new object[] { new DivisionExpression<IQuantity>(_ce, _ce), false},
                    new object[] { new EmptyExpression<IQuantity>(), false },
                    new object[] { new ExponentExpression<IQuantity>(_ce, 1.3), true },
                    new object[] { new InverseExpression<IQuantity>(_ce), true },
                    new object[] { new MultiplicationExpression<IQuantity>(_ce, _ce), false },
                    new object[] { new MultiplicationSequenceExpression<IQuantity>(_ce, _ce, _ce), false },
                    new object[] { new PrefixExpression<IQuantity>(Prefix.kilo, _ce), true },
                    new object[] { new ScaleExpression<IQuantity>(0.5, _ce), true },
                    //
                    new object[] { new ExponentExpression<IQuantity>(new ScaleExpression<IQuantity>(0.5, _ce), 2.0), true }
                };

            public static IEnumerable<object[]> TestData
            {
                get { return _data; }
            }
        }

        [Theory]
        [MemberData("TestData", MemberType = typeof(ConstantExpressionDataSource))]
        public void IsModifiedConstantExpression(Expression<IQuantity> e, bool expectedResult)
        {
            var result = e.IsModifiedConstantExpression();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ExtractConstantExpression()
        {
            var l = new Length();
            var complex = l.Constant()
                .Scale(0.5)
                .RaiseTo(2d);

            var content = complex.ExtractConstantExpression()?.Content;

            Assert.NotNull(content);
            Assert.Equal(l, content);
        }

    }
}