using System;

namespace Engineering.Expressions.Fluent
{
    /// <summary>
    ///     Mode of the expansion method
    /// </summary>
    [Flags]
    public enum ExpandOptions
    {
        /// <summary>Default expansion: scale * (multiplication)/(multiplication)</summary>
        Normal = 0,
        /// <summary>Agressive expansion: scale * multiplication</summary>
        Aggresive = 1,
        /// <summary>Prefix expansion: replace prefix with scale: kX -> 1000 * X</summary>
        Prefix = 2
    }
}