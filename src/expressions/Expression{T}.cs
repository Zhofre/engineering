using System;

namespace Engineering.Expressions
{
    /// <summary>
    ///     Expressions
    /// </summary>
    public abstract class Expression<T> : IExpressible
        where T : IExpressible
    {
        internal abstract bool RequiresBrackets { get; }
        public abstract bool CanScale { get; }
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

        internal string BracketedRepresentation
            => "(" + Representation + ")";

        internal string AutoBracketedRepresentation
            => RequiresBrackets ? BracketedRepresentation : Representation;
    }
}