using System;

namespace Journeys.Queries.Dtos
{
    public class JourneyWithLift
    {
        public JourneyWithLift(
            Guid journeyId, 
            Guid passengerId,
            DateTime dateOfOccurrence,
            decimal distance,
            string passengerName,
            decimal passengerLiftDistance)
        {
            JourneyId = journeyId;
            PassengerId = passengerId;
            DateOfOccurrence = dateOfOccurrence;
            Distance = distance;
            PassengerName = passengerName;
            PassengerLiftDistance = passengerLiftDistance;
        }

        public Guid JourneyId { get; private set; }

        public Guid PassengerId { get; private set; }

        public DateTime DateOfOccurrence { get; private set; }

        public decimal Distance { get; private set; }

        public string PassengerName { get; private set; }

        public decimal PassengerLiftDistance { get; private set; }
    }
}
