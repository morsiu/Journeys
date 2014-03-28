using Journeys.Queries.Dtos;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Journeys.Queries
{
    [DataContract]
    public class GetPeopleNamesQuery : IQuery<IEnumerable<PersonName>>
    {
    }
}
