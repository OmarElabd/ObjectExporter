using System.Collections.Generic;

namespace ObjectExporter.Core.Templates
{
    public static class ListExtensions
    {
        public static bool IsFirst<T>(this List<T> items, T item)
        {
            if (items.Count == 0)
                return false;
            T first = items[0];
            return item.Equals(first);
        }

        public static bool IsLast<T>(this List<T> items, T item)
        {
            if (items.Count == 0)
                return false;
            T last = items[items.Count - 1];
            return item.Equals(last);
        }
    }
}
