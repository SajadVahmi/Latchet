using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Latchet.Domain.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.Count() == 0;
        }

        public static bool IsNullOrEmpty<T>(this T[] source)
        {
            return source == null || source.Length == 0;
        }

        public static bool NotNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source != null && source.Count() > 0;
        }

        public static bool NotNullOrEmpty<T>(this T[] source)
        {
            return source != null && source.Length > 0;
        }

        public static bool ContainsAny<T>(this IEnumerable<T> source, params T[] items)
        {
            if (items.IsNullOrEmpty())
                return false;

            for (int i = 0; i < items.Length; i++)
            {
                if (source.Contains(items[i]))
                    return true;
            }

            return false;
        }

        public static bool ContainsAll<T>(this IEnumerable<T> source, params T[] items)
        {
            if (items.IsNullOrEmpty())
                return false;

            for (int i = 0; i < items.Length; i++)
            {
                if (!source.Contains(items[i]))
                    return false;
            }

            return true;
        }

        public static string JoinString<T>(this IEnumerable<T> source, string separator)
        {
            if (source == null) { return null; }
            if (source.Count() == 0) { return string.Empty; }

            return string.Join(separator, source);
        }
        public static string JoinDecriptionString<T>(this IEnumerable<T> source, string separator)where T: Enum
        {
            if (source == null) { return null; }
            if (source.Count() == 0) { return string.Empty; }
            var descriptionSource = source.Select(e => e.GetDescription());
            return string.Join(separator, descriptionSource);
        }
    }
}
