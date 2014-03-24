using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Policies;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Operations
{
    [Entity]
    internal class Journey
    {
        private static readonly Money _journeyCostPerUnitDistance = new Money(25m / 100m);
        private readonly Ride _ride;
        private readonly IId _id;

        public Journey(IId journeyId, Point journeyDistance)
        {
            _ride = new Ride(journeyDistance);
            _id = journeyId;
        }

        public IId Id { get { return _id; } }

        public void AddLift(IId passengerId, Distance distance)
        {
            _ride.IncludeLift(passengerId, distance);
        }

        public Money GetCostFor(IId passengerId)
        {
            var calculator = new PassengerLiftCostCalculator(_journeyCostPerUnitDistance, passengerId);
            _ride.Replay(calculator);
            return calculator.PassengerCost;
        }
    }
}
