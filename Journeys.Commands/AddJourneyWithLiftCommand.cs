using System;

namespace Journeys.Commands
{
    public class AddJourneyWithLiftCommand
    {
        public Guid JourneyId { get; private set; }

        public decimal RouteDistance { get; private set; }

        public DateTime DateOfOccurrence { get; private set; }

        public string PersonName { get; private set; }

        public decimal LiftDistance { get; private set; }

        public AddJourneyWithLiftCommand(
            Guid journeyId,
            decimal routeDistance,
            DateTime dateOfOccurrence,
            string personName,
            decimal liftDistance)
        {
            JourneyId = journeyId;
            RouteDistance = routeDistance;
            DateOfOccurrence = dateOfOccurrence;
            PersonName = personName;
            LiftDistance = liftDistance;
        }
    }
}
