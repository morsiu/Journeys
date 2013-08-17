using System;
using Journeys.Query.Messages;

namespace Journeys.Query.Infrastructure.Views
{
    internal class Nothing<T> : IMaybe<T>
    {
        public T Value
        {
            get { throw new InvalidOperationException(FailureMessages.NothingObjectHasNoValue); }
        }

        public bool HasValue
        {
            get { return false; }
        }
    }
}