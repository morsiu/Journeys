using System.Runtime.Serialization;

namespace Mors.Journeys.Data.Queries
{
    [DataContract]
    public sealed class GetPersonIdByNameQuery : IQuery<object>
    {
        [DataMember]
        public string PersonName { get; private set; }

        public GetPersonIdByNameQuery(string personName)
        {
            PersonName = personName;
        }
    }
}
