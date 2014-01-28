using System;
using System.Collections.Generic;

namespace Journeys.Query.Infrastructure.Views
{
    /// <summary>
    /// Stores values accessible by keys.
    /// The keys may be unrelated to values.
    /// </summary>
    internal class ValueLookup<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _values = new Dictionary<TKey, TValue>();

        public void Set(TKey key, TValue value)
        {
            _values[key] = value;
        }

        public IMaybe<TValue> Get(TKey key)
        {
            TValue value;
            if (_values.TryGetValue(key, out value))
            {
                return new Just<TValue>(value);
            }
            return new Nothing<TValue>();
        }

        public TValue GetOrAdd(TKey key, Func<TValue> newValue)
        {
            var result = Get(key);
            if (result.HasValue)
            {
                return result.Value;
            }
            var value = newValue();
            Set(key, value);
            return value;
        }

        public TValue Get(TKey key, Func<TValue> defaultValue)
        {
            var result = Get(key);
            if (result.HasValue)
            {
                return result.Value;
            }
            return defaultValue();
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> Retrieve()
        {
            return _values;
        }
    }
}
