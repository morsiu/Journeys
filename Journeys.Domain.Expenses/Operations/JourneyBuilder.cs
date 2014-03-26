using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Domain.Expenses.Operations
{
    public class JourneyBuilder
    {
        private readonly IId _journeyId;
        private readonly Point _distance;
        private readonly Dictionary<IId, decimal> _liftDistancesByPassengerId = new Dictionary<IId, decimal>();

        public JourneyBuilder(IId journeyId, decimal distance)
        {
            _journeyId = journeyId;
            _distance = new Point(distance);
        }

        public void AddLift(IId passengerId, decimal distance)
        {
            if (_liftDistancesByPassengerId.ContainsKey(passengerId))
            {
                throw new ArgumentException(Messages.JourneyAlreadyContainsLiftForThatPerson, "passengerId");
            }
            _liftDistancesByPassengerId[passengerId] = distance;
        }

        public Journey Build()
        {
            var lifts = _liftDistancesByPassengerId.Select(BuildLift);
            return new Journey(_journeyId, _distance, lifts);
        }

        private Lift BuildLift(KeyValuePair<IId, decimal> passengerIdAndDistance)
        {
            var liftDistance = new Distance(new Point(), new Point(passengerIdAndDistance.Value));
            return new Lift(passengerIdAndDistance.Key, liftDistance);
        }
    }
}
