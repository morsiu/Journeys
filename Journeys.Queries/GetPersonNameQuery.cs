using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Queries
{
    public class GetPersonNameQuery : IQuery<string>
    {
        public Guid PersonId { get; private set; }

        public GetPersonNameQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
