using System;

namespace Engineering.Units.Expressions
{
    /// <summary>
    ///     Expressions
    /// </summary>
    public abstract class Expression<T> : IExpressible
        where T : IExpressible
    {
        public abstract bool CanScale { get; }
        public abstract string Representation { get; }
        public abstract Expression<TOther> Cast<TOther>(Func<T, TOther> f)
            where TOther : IExpressible;
    }
}