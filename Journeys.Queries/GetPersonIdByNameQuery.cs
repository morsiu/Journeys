using System;
using Journeys.Common;

namespace Journeys.Queries
{
    public class GetPersonIdByNameQuery : IQuery<IId>
    {
        public string PersonName { get; private set; }

        public GetPersonIdByNameQuery(string personName)
        {
            PersonName = personName;
        }
    }
}
