using System.Collections.Generic;
using System.Runtime.Serialization;
using Journeys.Data.Queries.Dtos;

namespace Journeys.Data.Queries
{
    [DataContract]
    public sealed class GetPeopleNamesQuery : IQuery<IEnumerable<PersonName>>
    {
    }
}
