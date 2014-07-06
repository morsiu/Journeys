using System.Runtime.Serialization;

namespace Journeys.Data.Queries
{
    [DataContract]
    public class GetPersonIdByNameQuery : IQuery<object>
    {
        [DataMember]
        public string PersonName { get; private set; }

        public GetPersonIdByNameQuery(string personName)
        {
            PersonName = personName;
        }
    }
}
