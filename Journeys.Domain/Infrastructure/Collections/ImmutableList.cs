using System.Collections;
using System.Collections.Generic;

namespace Journeys.Domain.Infrastructure.Collections
{
    public class ImmutableList<T> : IEnumerable<T>
    {
        private readonly Item _head;
        public static readonly ImmutableList<T> Empty = new ImmutableList<T>(null);

        private ImmutableList(Item head)
        {
            _head = head;
        }

        public ImmutableList<T> Add(T value)
        {
            var item = new Item(value, _head);
            return new ImmutableList<T>(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var item = _head;
            while (item != null)
            {
                yield return item.Value;
                item = item.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Item
        {
            public Item(T value, Item next)
            {
                Value = value;
                Next = next;
            }

            public T Value { get; private set; }
            public Item Next { get; private set; }
        }

    }
}
