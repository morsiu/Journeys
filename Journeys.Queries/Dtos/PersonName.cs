using System;
using Journeys.Common;

namespace Journeys.Queries.Dtos
{
    public class PersonName
    {
        public PersonName(IId ownerId, string name)
        {
            OwnerId = ownerId;
            Name = name;
        }

        public IId OwnerId { get; private set; }

        public string Name { get; private set; }
    }
}
