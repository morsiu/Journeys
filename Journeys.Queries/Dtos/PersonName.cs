using System;

namespace Journeys.Queries.Dtos
{
    public class PersonName
    {
        public PersonName(Guid ownerId, string name)
        {
            OwnerId = ownerId;
            Name = name;
        }

        public Guid OwnerId { get; private set; }

        public string Name { get; private set; }
    }
}
