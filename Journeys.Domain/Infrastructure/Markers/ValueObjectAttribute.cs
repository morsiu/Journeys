using System;

namespace Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    internal class ValueObjectAttribute : Attribute
    {
    }
}
