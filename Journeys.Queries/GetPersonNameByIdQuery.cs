using System;

namespace Journeys.Queries
{
    public class GetPersonNameByIdQuery : IQuery<string>
    {
        public Guid PersonId { get; private set; }

        public GetPersonNameByIdQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
