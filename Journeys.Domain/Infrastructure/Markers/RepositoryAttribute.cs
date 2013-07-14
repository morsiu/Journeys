using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Infrastructure.Markers
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class RepositoryAttribute : Attribute
    {
    }
}
