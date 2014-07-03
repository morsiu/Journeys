using Journeys.Commands.Dtos;
using Journeys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Journeys.Commands
{
    [DataContract]
    public class AddJourneyWithLiftsCommand
    {
        [DataMember]
        public object JourneyId { get; private set; }

        [DataMember]
        public decimal RouteDistance { get; private set; }

        [DataMember]
        public DateTime DateOfOccurrence { get; private set; }

        [DataMember]
        public IReadOnlyList<Lift> Lifts { get; private set; }

        public AddJourneyWithLiftsCommand(
            object journeyId,
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
