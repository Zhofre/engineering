using System;
using System.Reflection;
using Engineering.Expressions.Attributes;

namespace Engineering.Expressions
{
    public static class Utility
    {
        internal static T GetAttribute<T>(this Enum enumValue)
        where T : Attribute
        {
            return enumValue
                .GetType()
                .GetTypeInfo()
                .GetDeclaredField(enumValue.ToString())
                .GetCustomAttribute<T>();
        }


        public static bool Equals(double v1, double v2, double epsilon = 1e-9)
        {
            return Math.Abs(v1 - v2) < epsilon;
        }

        public static double Value(this Prefix prefix)
            => prefix.GetAttribute<PrefixInformationAttribute>().Factor;
    }

}