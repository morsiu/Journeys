using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Exceptions
{
    /// <summary>
    /// Exception used by factories to report failure to build new entity.
    /// </summary>
    /// <remarks>
    /// The exception message must denote what kind of object failed to build and the reason for that.
    /// </remarks>
    public class EntityBuildException : Exception
    {
        public EntityBuildException(string message)
            : base(message)
        {
        }
    }
}
