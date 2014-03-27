using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Capabilities.Journeys.Events;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Policies
{
    [Policy]
    public class PassengerLiftCostCalculator : IJourneyCostCalculator
    {
        private readonly IId _passengerId;
        private readonly Money _journeyCostPerKilometer;

        public PassengerLiftCostCalculator(Money journeyCostPerKilometer, IId passengerId)
        {
            _journeyCostPerKilometer = journeyCostPerKilometer;
            _passengerId = passengerId;
        }

        public Expense Calculate(Journey journey)
        {
            var visitor = new JourneyVisitor(_journeyCostPerKilometer, _passengerId);
            journey.Visit(visitor);
            var liftId = new LiftId(journey.Id, _passengerId);
            return new Expense(liftId, visitor.PassengerCost);
        }

        private struct JourneyVisitor : IJourneyVisitor
        {
            private readonly IId _passengerId;
            private readonly Money _journeyCostPerKilometer;
            private bool _isPassengerOnBoard;
            private int _onBoardPassengerCount;
            private Money _passengerCost;

            public JourneyVisitor(Money journeyCostPerKilometer, IId passengerId)
            {
                _journeyCostPerKilometer = journeyCostPerKilometer;
                _passengerId = passengerId;
                _isPassengerOnBoard = false;
                _onBoardPassengerCount = 1;
                _passengerCost = new Money();
            }

            private void IncreasePassengerCost(decimal driveDistance)
            {
                var driveCost = _journeyCostPerKilometer * driveDistance;
                var passengerCost = driveCost / _onBoardPassengerCount;
                _passengerCost += passengerCost;
            }

            public Money PassengerCost { get { return _passengerCost; } }

            void IJourneyVisitor.Visit(Drive drive)
            {
                if (_isPassengerOnBoard) IncreasePassengerCost(drive.Distance.Length);
            }

            void IJourneyVisitor.Visit(PassengerExit exit)
            {
                _onBoardPassengerCount -= 1;
                if (exit.PassengerId.Equals(_passengerId)) _isPassengerOnBoard = false;
            }

            void IJourneyVisitor.Visit(PassengerPickup pickup)
            {
                _onBoardPassengerCount += 1;
                if (pickup.PassengerId.Equals(_passengerId)) _isPassengerOnBoard = true;
            }

            void IJourneyVisitor.Visit(JourneyStart start)
            {
            }

            void IJourneyVisitor.Visit(JourneyFinish finish)
            {
            }
        }
    }
}
