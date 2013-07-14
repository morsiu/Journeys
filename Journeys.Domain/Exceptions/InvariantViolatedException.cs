using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Exceptions
{
    /// <summary>
    /// Exceptions used by bussiness objects to report failure of performed operation because of invariant violation.
    /// </summary>
    public class InvariantViolatedException : Exception
    {
        public InvariantViolatedException(string message)
            : base(message)
        {
        }
    }
}
