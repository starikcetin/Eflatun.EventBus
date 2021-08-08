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
    }
}
