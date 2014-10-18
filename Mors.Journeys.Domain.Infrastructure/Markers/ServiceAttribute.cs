using System;

namespace Mors.Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class ServiceAttribute : Attribute
    {
    }
}
