using System;

namespace Journeys.Queries
{
    public class GetPersonIdByNameQuery : IQuery<Guid?>
    {
        public string PersonName { get; private set; }

        public GetPersonIdByNameQuery(string personName)
        {
            PersonName = personName;
        }
    }
}
