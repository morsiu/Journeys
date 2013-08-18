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

        public void Update(TKey key, Func<TValue, TValue> updater)
        {
            var value = _values[key];
            var newKey = _keyGenerator(value);
            _values.Remove(key);
            _values[newKey] = updater(value);
        }

        public void UpdateOrAdd(TKey key, Func<TValue> newValue, Func<TValue, TValue> update)
        {
            var value = !_values.ContainsKey(key) ? newValue() : _values[key];
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
            if (_values.ContainsKey(key))
            {
                return _values[key];
            }
            var value = defaultValue();
            return value;
        }

        public TValue Remove(TKey key)
        {
            var value = _values[key];
            _values.Remove(key);
            return value;
        }

        public IEnumerable<TValue> Retrieve()
        {
            return _values.Values;
        }
    }
}
