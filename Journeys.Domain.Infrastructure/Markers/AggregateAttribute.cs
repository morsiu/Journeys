using System;

namespace Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AggregateAttribute : Attribute
    {
    }
}
