using System;

namespace Journeys.Queries.Dtos
{
    public class PersonName
    {
        public PersonName(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}
