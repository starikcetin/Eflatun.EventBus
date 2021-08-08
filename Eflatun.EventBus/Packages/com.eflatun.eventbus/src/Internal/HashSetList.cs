using System.Collections.Generic;

namespace Eflatun.EventBus.Internal
{
    internal class HashSetList<T> : HashSet<T>, IReadOnlyList<T>
    {
        private readonly List<T> _elmList = new List<T>();

        public void UnionWith(HashSetList<T> other)
        {
            for (var i = 0; i < other._elmList.Count; i++)
            {
                Add(other._elmList[i]);
            }
        }

        public new void Add(T item)
        {
            if (base.Add(item))
            {
                _elmList.Add(item);
            }
        }

        public new void Remove(T item)
        {
            if (base.Remove(item))
            {
                _elmList.Remove(item);
            }
        }

        public new void Clear()
        {
            base.Clear();
            _elmList.Clear();
        }

        public T this[int index] => _elmList[index];
    }
}
