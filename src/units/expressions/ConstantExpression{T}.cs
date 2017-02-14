using System;

namespace Engineering.Units.Expressions
{
    public sealed class ConstantExpression<T> : Expression<T>
        where T : IExpressible
    {
        public ConstantExpression(T content)
        {
            Content = content;
        }

        public T Content { get; }

        public override bool CanPrefix => Content.CanPrefix;

        public override string Representation => Content.Representation;

        public override Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            => new ConstantExpression<TOther>(f(Content));

    }
}