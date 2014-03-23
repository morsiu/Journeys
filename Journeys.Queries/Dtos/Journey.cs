using Journeys.Common;
using System;
using System.Collections.Generic;

namespace Journeys.Queries.Dtos
{
    public class Journey
    {
        public Journey(IId id, DateTime dateOfOccurrence, decimal routeDistance, IReadOnlyCollection<Lift> lifts)
        {
            Id = id;
            DateOfOccurrence = dateOfOccurrence;
            RouteDistance = routeDistance;
            Lifts = lifts;
        }

        public IId Id { get; private set; }

        public DateTime DateOfOccurrence { get; private set; }

        public decimal RouteDistance { get; private set; }

        public IReadOnlyCollection<Lift> Lifts { get; private set; }
    }
}
