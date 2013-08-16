using System;

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
