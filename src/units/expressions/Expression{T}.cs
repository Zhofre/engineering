using System;

namespace Engineering.Units.Expressions
{
    /// <summary>
    ///     Expressions
    /// </summary>
    public abstract class Expression<T> : IExpressible
        where T : IExpressible
    {
        public abstract bool CanPrefix { get; }
        public abstract string Representation { get; }
        public abstract Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            where TOther : IExpressible;

        public static implicit operator Expression<T>(T other)
        {
            var derOther = other as IDerived<T>;
            return derOther == null
                ? new ConstantExpression<T>(other)
                : derOther.Expression;
        }

        public static Expression<T> operator *(Prefix prefix, Expression<T> expression)
        {
            return new PrefixExpression<T>(prefix, expression);
        }
    }
}