using System;

namespace Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    internal class ServiceAttribute : Attribute
    {
    }
}
