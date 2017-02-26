using System;

namespace Engineering.Expressions.Fluent
{
    [Flags]
    public enum CompactionOptions
    {
        Normal = 0,
        CompactPrefixes = 1
    }
}