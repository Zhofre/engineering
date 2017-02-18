using System;

namespace Engineering.Expressions
{
    public sealed class ConstantExpression<T> : Expression<T>
        where T : IExpressible
    {
        public ConstantExpression(T content)
        {
            Content = content;
        }

        public T Content { get; }

        public override bool CanScale => Content.CanScale;

        public override string Representation => Content.Representation;

        internal override bool RequiresBrackets => false;

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new ConstantExpression<TOther>(f(Content));

    }
}