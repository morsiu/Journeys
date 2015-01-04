using System.Collections.Generic;
using System.Runtime.Serialization;
using Mors.Journeys.Common;
using Mors.Journeys.Data.Queries.Dtos;

namespace Mors.Journeys.Data.Queries
{
    [DataContract]
    public sealed class GetPeopleNamesQuery : IQuery<IEnumerable<PersonName>>
    {
    }
}
