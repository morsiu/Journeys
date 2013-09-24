using System;

namespace Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AggregateAttribute : Attribute
    {
    }
}
