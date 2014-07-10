using System;
using System.Collections.Generic;
using Journeys.Application.Query.Messages;

namespace Journeys.Application.Query.Infrastructure.Views
{
    internal sealed class ValueMultiSet<TKey, TValue>
    {
        private static readonly IReadOnlyCollection<TValue> _emptyList = new List<TValue>();
        private readonly Func<TValue, TKey> _keyGenerator;
        private readonly Dictionary<TKey, List<TValue>> _values;

        public ValueMultiSet(Func<TValue, TKey> keyGenerator)
        {
            if (keyGenerator.IsNull()) throw new ArgumentNullException("keyGenerator");

            _keyGenerator = keyGenerator;
            _values = new Dictionary<TKey, List<TValue>>();
        }

        public void Add(TValue value)
        {
            var key = _keyGenerator(value);
            if (key.IsNull()) throw new ArgumentException(FailureMessages.CannotAddValueWithNullKey, "value");
            var list = AddOrGetList(key);
            list.Add(value);
        }

        private ICollection<TValue> AddOrGetList(TKey key)
        {
            List<TValue> list;
            if (_values.TryGetValue(key, out list))
            {
                return list;
            }
            var newList = new List<TValue>();
            _values[key] = newList;
            return newList;
        }

        public IReadOnlyCollection<TValue> Get(TKey key)
        {
            return _values[key];
        }

        public IReadOnlyCollection<TValue> GetOrDefault(TKey key)
        {
            return HasValueForKey(key) ? _values[key] : _emptyList;
        }

        private bool HasValueForKey(TKey key)
        {
            return key.IsNotNull() && _values.ContainsKey(key);
        }
    }
}
