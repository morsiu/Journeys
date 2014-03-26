using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Policies;
using Journeys.Domain.Infrastructure.Markers;
using System.Collections.Generic;

namespace Journeys.Domain.Expenses.Operations
{
    [Entity]
    public class Journey
    {
        private static readonly Money _journeyCostPerUnitDistance = new Money(25m / 100m);
        private readonly Ride _ride;
        private readonly IId _id;

        internal Journey(IId journeyId, Point journeyDistance, IEnumerable<Lift> lifts)
        {
            _id = journeyId;
            _ride = new Ride(journeyDistance);
            foreach (var lift in lifts)
            {
                _ride.IncludeLift(lift);
            }
        }

        public IId Id { get { return _id; } }

        public Expense GetCostFor(IId passengerId)
        {
            var calculator = new PassengerLiftCostCalculator(_journeyCostPerUnitDistance, passengerId);
            _ride.Replay(calculator);
            return new Expense(new LiftId(_id, passengerId), calculator.PassengerCost);
        }
    }
}
