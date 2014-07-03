using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Capabilities.Journeys.Events;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Policies
{
    [Policy]
    public class PassengerLiftCostCalculator : IJourneyCostCalculator
    {
        private readonly object _passengerId;
        private readonly Money _journeyCostPerKilometer;

        public PassengerLiftCostCalculator(Money journeyCostPerKilometer, object passengerId)
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

        private class JourneyVisitor : IJourneyVisitor
        {
            private readonly object _passengerId;
            private readonly Money _journeyCostPerKilometer;
            private bool _isPassengerOnBoard;
            private int _onBoardPersonCount;
            private Money _passengerCost;

            public JourneyVisitor(Money journeyCostPerKilometer, object passengerId)
            {
                _journeyCostPerKilometer = journeyCostPerKilometer;
                _passengerId = passengerId;
                _isPassengerOnBoard = false;
                _onBoardPersonCount = 1; // Count the driver
                _passengerCost = new Money();
            }

            private void IncreasePassengerCost(Distance driveDistance)
            {
                var driveCost = _journeyCostPerKilometer * driveDistance.Amount;
                var passengerCost = driveCost / _onBoardPersonCount;
                _passengerCost += passengerCost;
            }

            public Money PassengerCost { get { return _passengerCost; } }

            void IJourneyVisitor.Visit(Drive drive)
            {
                if (_isPassengerOnBoard) IncreasePassengerCost(drive.Distance.Length);
            }

            void IJourneyVisitor.Visit(PassengerExit exit)
            {
                _onBoardPersonCount -= 1;
                if (exit.PassengerId.Equals(_passengerId)) _isPassengerOnBoard = false;
            }

            void IJourneyVisitor.Visit(PassengerPickup pickup)
            {
                _onBoardPersonCount += 1;
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
