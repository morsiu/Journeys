using System;

namespace Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ValueObjectAttribute : Attribute
    {
    }
}
