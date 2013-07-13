using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Markers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    internal class ValueObjectAttribute : Attribute
    {
    }
}
