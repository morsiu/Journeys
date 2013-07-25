using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Queries
{
    public class GetIdOfPersonWithNameQuery : IQuery<Guid?>
    {
        public string PersonName { get; private set; }

        public GetIdOfPersonWithNameQuery(string personName)
        {
            PersonName = personName;
        }
    }
}
