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
            var result = e.IsConstantOrUnaryExpression();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ExtractConstantExpression()
        {
            var l = new Length();
            var complex = l.Constant<IQuantity>()
                .Scale(0.5)
                .Pow(2d);

            var content = complex.ExtractConstantExpression()?.Content;

            Assert.NotNull(content);
            Assert.Equal(l, content);
        }

        [Fact]
        public void CreateExpressionGroups1()
        {
            var l = new Length();
            var ce = l.Constant<IQuantity>();
            
            var sut = new Expression<IQuantity>[]
            {
                ce,
                ce.RaiseTo(1.3d),
                ce.Invert(),
                ce.Scale(0.5),
                ce.Prefix(Prefix.kilo)
            };

            var result = sut.CreateExpressionGroups();

            Assert.Equal(1, result.Count);
            Assert.Equal(5, result[0].Expressions.Count);
        }

        [Fact]
        public void CreateExpressionGroups2()
        {
            var l = new Length();
            var ce = l.Constant<IQuantity>();
            
            var sut = new Expression<IQuantity>[]
            {
                ce,
                ce.RaiseTo(1.3d),
                ce.Invert(),
                ce.Scale(0.5),
                ce.Prefix(Prefix.kilo),
                ce.MultiplyWith(ce)
            };

            var result = sut.CreateExpressionGroups();

            Assert.Equal(2, result.Count);
            Assert.Equal(5, result[0].Expressions.Count);
            Assert.Null(result[1].Item);
        }

        [Fact]
        public void CreateExpressionGroups3()
        {
            var l = new Length();
            var t = new Time();
            var cel = l.Constant<IQuantity>();
            var cet = t.Constant<IQuantity>();
            
            var sut = new Expression<IQuantity>[]
            {
                cel,
                cel.RaiseTo(1.3d),
                cel.Invert(),
                cel.Scale(0.5),
                cel.Prefix(Prefix.kilo),
                cel.MultiplyWith(cet),
                cet,
                cet.RaiseTo(1.3d),
                cet.Invert(),
                cet.Scale(0.5)
            };

            var result = sut.CreateExpressionGroups();

            Assert.Equal(3, result.Count);
            Assert.Equal(l, result[0].Item);
            Assert.Equal(5, result[0].Expressions.Count);
            Assert.Equal(t, result[1].Item);
            Assert.Equal(4, result[1].Expressions.Count);
            Assert.Null(result[2].Item);
            Assert.Equal(1, result[2].Expressions.Count);
        }

    }
}