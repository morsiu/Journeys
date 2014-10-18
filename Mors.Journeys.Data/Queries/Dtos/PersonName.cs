using System.Runtime.Serialization;

namespace Mors.Journeys.Data.Queries.Dtos
{
    [DataContract]
    public sealed class PersonName
    {
        public PersonName(object ownerId, string name)
        {
            OwnerId = ownerId;
            Name = name;
        }

        [DataMember]
        public object OwnerId { get; private set; }

        [DataMember]
        public string Name { get; private set; }
    }
}
