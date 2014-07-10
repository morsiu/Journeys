using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Journeys.Data.Queries.Dtos
{
    [DataContract]
    public sealed class Journey
    {
        public Journey(object id, DateTime dateOfOccurrence, decimal routeDistance, IReadOnlyCollection<Lift> lifts)
        {
            Id = id;
            DateOfOccurrence = dateOfOccurrence;
            RouteDistance = routeDistance;
            Lifts = lifts;
        }

        [DataMember]
        public object Id { get; private set; }

        [DataMember]
        public DateTime DateOfOccurrence { get; private set; }

        [DataMember]
        public decimal RouteDistance { get; private set; }

        [DataMember]
        public IReadOnlyCollection<Lift> Lifts { get; private set; }
    }
}
