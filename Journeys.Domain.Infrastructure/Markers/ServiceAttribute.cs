using System;

namespace Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class ServiceAttribute : Attribute
    {
    }
}
