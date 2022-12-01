using System;
using System.Collections.Generic;

namespace Dinota.Core.Extensions
{
    public static class CoreExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static void ThrowIfNull<T>(this T value, string name) where T : class
        {
            if (value == null) throw new ArgumentNullException(name);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            collection.ThrowIfNull("collection");
            action.ThrowIfNull("action");

            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}
