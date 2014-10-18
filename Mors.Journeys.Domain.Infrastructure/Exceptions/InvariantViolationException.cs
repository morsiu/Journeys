using System;

namespace Mors.Journeys.Domain.Infrastructure.Exceptions
{
    /// <summary>
    /// Exceptions used by bussiness objects to report failure of performed operation because of invariant violation.
    /// </summary>
    [Serializable]
    public sealed class InvariantViolationException : Exception
    {
        public InvariantViolationException(string message)
            : base(message)
        {
        }
    }
}
