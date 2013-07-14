using System;

namespace Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class AggregateAttribute : Attribute
    {
    }
}
