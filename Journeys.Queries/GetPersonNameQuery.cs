using System;

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
