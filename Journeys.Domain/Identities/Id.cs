using Journeys.Domain.Markers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Identities
{
    [ValueObject]
    public struct Id<T>
    {
        private int _id;

        public Id(int id)
        {
            _id = id;
        }
    }
}
