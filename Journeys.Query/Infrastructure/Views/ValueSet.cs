using System;
using System.Collections.Generic;
using Journeys.Query.Messages;

namespace Journeys.Query.Infrastructure.Views
{
    /// <summary>
    /// Stores values that are accessible by keys.
    /// The keys are computed from values.
    /// </summary>
    internal class ValueSet<TKey, TValue>
    {
        private readonly Func<TValue, TKey> _keyGenerator;
        private readonly Dictionary<TKey, TValue> _values;

        public ValueSet(Func<TValue, TKey> keyGenerator)
        {
            if (keyGenerator.IsNull()) throw new ArgumentNullException("keyGenerator");

            _keyGenerator = keyGenerator;
            _values = new Dictionary<TKey, TValue>();
        }

        public void Add(TValue value)
        {
            var key = _keyGenerator(value);
            if (key.IsNull()) throw new ArgumentException(FailureMessages.CannotAddValueWithNullKey, "value");
            _values.Add(key, value);
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
