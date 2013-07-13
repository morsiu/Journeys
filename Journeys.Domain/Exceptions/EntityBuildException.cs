using System;

namespace Journeys.Domain.Exceptions
{
    /// <summary>
    /// Exception used by factories to report failure to build new entity.
    /// </summary>
    /// <remarks>
    /// The exception message must denote what kind of entity failed to build and the reason for that.
    /// </remarks>
    public class EntityBuildException : Exception
    {
        public EntityBuildException(string message)
            : base(message)
        {
        }
    }
}
