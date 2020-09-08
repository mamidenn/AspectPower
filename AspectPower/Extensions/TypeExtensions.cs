using System;

namespace AspectPower.Extensions
{
    public static class TypeExtensions
    {
        public static bool Is<T>(this Type t) => typeof(T).IsAssignableFrom(t);
    }
}