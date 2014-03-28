using Journeys.Common;
using System.Runtime.Serialization;

namespace Journeys.Queries.Dtos
{
    [DataContract]
    public class PersonName
    {
        public PersonName(IId ownerId, string name)
        {
            OwnerId = ownerId;
            Name = name;
        }

        [DataMember]
        public IId OwnerId { get; private set; }

        [DataMember]
        public string Name { get; private set; }
    }
}
