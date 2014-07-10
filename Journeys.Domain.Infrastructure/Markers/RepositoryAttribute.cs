using System;

namespace Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Interface)]
    public sealed class RepositoryAttribute : Attribute
    {
    }
}
