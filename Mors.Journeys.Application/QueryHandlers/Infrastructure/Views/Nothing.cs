using System;
using Mors.Journeys.Application.QueryHandlers.Messages;

namespace Mors.Journeys.Application.QueryHandlers.Infrastructure.Views
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