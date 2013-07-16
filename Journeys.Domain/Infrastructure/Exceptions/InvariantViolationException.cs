using System;

namespace Journeys.Domain.Infrastructure.Exceptions
{
    /// <summary>
    /// Exceptions used by bussiness objects to report failure of performed operation because of invariant violation.
    /// </summary>
    public class InvariantViolationException : Exception
    {
        public InvariantViolationException(string message)
            : base(message)
        {
        }
    }
}
