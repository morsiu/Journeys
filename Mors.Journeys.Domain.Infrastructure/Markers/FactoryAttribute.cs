using System;

namespace Mors.Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class FactoryAttribute : Attribute
    {
    }
}
