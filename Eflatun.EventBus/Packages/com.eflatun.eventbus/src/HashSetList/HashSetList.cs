using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class HashSetList<T> : HashSet<T>, IReadOnlyList<T>
    {
        private List<T> _elmList = new List<T>();

        public void UnionWith(HashSetList<T> other)
        {
            for (var i = 0; i < other._elmList.Count; i++)
            {
                var otherItem = other._elmList[i];
                Add(otherItem);
            }
        }

        public new void Add(T item)
        {
            var added = base.Add(item);
            if (added)
            {
                _elmList.Add(item);
            }
        }

        public new void Remove(T item)
        {
            var removed = base.Remove(item);
            if (removed)
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
