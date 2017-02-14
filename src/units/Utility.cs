using System;
using System.Reflection;

namespace Engineering.Units
{
    internal static class Utility
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


        internal static bool Equals(double v1, double v2, double epsilon = 1e-9)
        {
            return Math.Abs(v1 - v2) < epsilon;
        }
    }

}