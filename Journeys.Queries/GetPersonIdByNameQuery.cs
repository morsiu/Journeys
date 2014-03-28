using Journeys.Common;
using System.Runtime.Serialization;

namespace Journeys.Queries
{
    [DataContract]
    public class GetPersonIdByNameQuery : IQuery<IId>
    {
        public string PersonName { get; private set; }

        public GetPersonIdByNameQuery(string personName)
        {
            PersonName = personName;
        }
    }
}
