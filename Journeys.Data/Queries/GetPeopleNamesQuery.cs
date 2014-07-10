using Journeys.Data.Queries.Dtos;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Journeys.Data.Queries
{
    [DataContract]
    public sealed class GetPeopleNamesQuery : IQuery<IEnumerable<PersonName>>
    {
    }
}
