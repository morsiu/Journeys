using System;
using System.Collections.Generic;

namespace Journeys.Query.Infrastructure.Views
{
    /// <summary>
    /// Stores values that are accessible by keys.
    /// The keys are computed from values.
    /// </summary>
    internal class Set<TKey, TValue>
    {
        private readonly Func<TValue, TKey> _keyGenerator;
        private readonly Dictionary<TKey, TValue> _values;

        public Set(Func<TValue, TKey> keyGenerator)
        {
            _keyGenerator = keyGenerator;
            _values = new Dictionary<TKey, TValue>();
        }

        public void Add(TValue value)
        {
            var key = _keyGenerator(value);
            _values.Add(key, value);
        }
       
        public void UpdateOrAdd(TKey key, Func<TValue> newValue, Func<TValue, TValue> update)
        {
            var value = HasValueForKey(key) ? _values[key] : newValue();
            var updatedValue = update(value);
            var updatedValueKey = _keyGenerator(updatedValue);
            _values[updatedValueKey] = updatedValue;
        }

        public TValue Get(TKey key)
        {
            return _values[key];
        }

        public TValue Get(TKey key, Func<TValue> defaultValue)
        {
            return HasValueForKey(key) ? _values[key] : defaultValue();
        }
        
        public IEnumerable<TValue> Retrieve()
        {
            return _values.Values;
        }

        private bool HasValueForKey(TKey key)
        {
            return key.IsNotNull() && _values.ContainsKey(key);
        }
    }
}
