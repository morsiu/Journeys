using System;

namespace Journeys.Domain.Markers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    internal class ValueObjectAttribute : Attribute
    {
    }
}
