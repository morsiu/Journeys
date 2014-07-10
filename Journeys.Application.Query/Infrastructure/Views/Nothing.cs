using System;
using Journeys.Application.Query.Messages;

namespace Journeys.Application.Query.Infrastructure.Views
{
    internal sealed class Nothing<T> : IMaybe<T>
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