using System.Collections.Generic;

namespace Eflatun.EventBus
{
    internal static class Utils
    {
        internal static readonly ISet<int> EmptyIntSet = new HashSet<int>();

        internal static HashSet<T> ToHashSet<T>(this T val)
        {
            return new HashSet<T>(new [] {val});
        }

        internal static void UnionWith<T>(this List<T> writeList, List<T> otherList)
        {
            for (var i = 0; i < otherList.Count; i++)
            {
                var x1 = otherList[i];
                if (!writeList.Contains(x1))
                {
                    writeList.Add(x1);
                }
            }
        }

        internal static void AddIfNotContains<T>(this List<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }
    }
}
