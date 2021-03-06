﻿using System;
using System.Collections.Generic;
using System.Linq;
using Mors.Journeys.Domain.Expenses.Capabilities;
using Mors.Journeys.Domain.Expenses.Capabilities.Journeys;
using Mors.Journeys.Domain.Infrastructure.Markers;

namespace Mors.Journeys.Domain.Expenses.Operations
{
    [Factory]
    public sealed class JourneyBuilder
    {
        private readonly object _journeyId;
        private readonly Distance _routeDistance;
        private readonly Dictionary<object, Distance> _liftDistancesByPassengerId = new Dictionary<object, Distance>();

        public JourneyBuilder(object journeyId, decimal routeDistance)
        {
            _journeyId = journeyId;
            _routeDistance = new Distance(routeDistance);
        }

        public void AddLift(object passengerId, decimal distance)
        {
            if (_liftDistancesByPassengerId.ContainsKey(passengerId))
            {
                throw new ArgumentException(Messages.JourneyAlreadyContainsLiftForThatPerson, "passengerId");
            }
            _liftDistancesByPassengerId[passengerId] = new Distance(distance);
        }

        public Journey Build()
        {
            var lifts = _liftDistancesByPassengerId.Select(BuildLift);
            return new Journey(_journeyId, _routeDistance, lifts);
        }

        private static Lift BuildLift(KeyValuePair<object, Distance> passengerIdAndDistance)
        {
            var liftDistance = new RouteDistance(new RoutePoint(), new RoutePoint(passengerIdAndDistance.Value));
            return new Lift(passengerIdAndDistance.Key, liftDistance);
        }
    }
}
