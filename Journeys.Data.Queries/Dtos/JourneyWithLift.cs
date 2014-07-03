using System;
using System.Runtime.Serialization;

namespace Journeys.Data.Queries.Dtos
{
    [DataContract]
    public class JourneyWithLift
    {
        public JourneyWithLift(
            object journeyId,
            object passengerId,
            DateTime dateOfOccurrence,
            decimal routeDistance,
            string passengerName,
            decimal passengerLiftDistance)
        {
            JourneyId = journeyId;
            PassengerId = passengerId;
            DateOfOccurrence = dateOfOccurrence;
            RouteDistance = routeDistance;
            PassengerName = passengerName;
            PassengerLiftDistance = passengerLiftDistance;
        }

        [DataMember]
        public object JourneyId { get; private set; }

        [DataMember]
        public object PassengerId { get; private set; }

        [DataMember]
        public DateTime DateOfOccurrence { get; private set; }

        [DataMember]
        public decimal RouteDistance { get; private set; }

        [DataMember]
        public string PassengerName { get; private set; }

        [DataMember]
        public decimal PassengerLiftDistance { get; private set; }
    }
}
