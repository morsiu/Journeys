using System;

namespace Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ValueObjectAttribute : Attribute
    {
    }
}
