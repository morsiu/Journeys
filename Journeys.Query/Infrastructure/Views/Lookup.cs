using System;
using System.Collections.Generic;

namespace Journeys.Query.Infrastructure.Views
{
    internal class Lookup<TKey, TValue>
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

        public TValue Get(TKey key, Func<TValue> defaultValue)
        {
            var result = Get(key);
            if (result.HasValue)
            {
                return result.Value;
            }
            return defaultValue();
        }
    }
}
