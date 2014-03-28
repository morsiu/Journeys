using Journeys.Common;
using System;
using System.Runtime.Serialization;

namespace Journeys.Queries.Dtos
{
    [DataContract]
    public class JourneyWithLift
    {
        public JourneyWithLift(
            IId journeyId,
            IId passengerId,
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
        public IId JourneyId { get; private set; }

        [DataMember]
        public IId PassengerId { get; private set; }

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
