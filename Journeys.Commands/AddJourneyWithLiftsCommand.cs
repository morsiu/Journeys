using System;
using System.Collections.Generic;
using System.Linq;
using Journeys.Common;
using Journeys.Commands.Dtos;

namespace Journeys.Commands
{
    public class AddJourneyWithLiftsCommand
    {
        public IId JourneyId { get; private set; }

        public decimal RouteDistance { get; private set; }

        public DateTime DateOfOccurrence { get; private set; }

        public IReadOnlyList<Lift> Lifts { get; private set; }

        public AddJourneyWithLiftsCommand(
            IId journeyId,
            decimal routeDistance,
            DateTime dateOfOccurrence,
            IEnumerable<Lift> lifts)
        {
            JourneyId = journeyId;
            RouteDistance = routeDistance;
            DateOfOccurrence = dateOfOccurrence;
            Lifts = lifts.ToList();
        }
    }
}
