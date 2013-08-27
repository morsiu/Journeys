using System;
using Journeys.Common;

namespace Journeys.Queries
{
    public class GetPersonNameByIdQuery : IQuery<string>
    {
        public IId PersonId { get; private set; }

        public GetPersonNameByIdQuery(IId personId)
        {
            PersonId = personId;
        }
    }
}
