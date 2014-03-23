using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Capabilities.RideEvents;

namespace Journeys.Domain.Expenses.Policies
{
    internal class PassengerLiftCostCalculator : IRideVisitor
    {
        private readonly Money _journeyCostPerKilometer;
        private readonly IId _passengerId;
        private Money _passengerCost;
        private bool _isPassengerOnBoard;
        private int _onBoardPassengerCount;

        public PassengerLiftCostCalculator(Money journeyCostPerKilometer, IId passengerId)
        {
            _journeyCostPerKilometer = journeyCostPerKilometer;
            _passengerId = passengerId;
        }

        public void Visit(Drive drive)
        {
            if (_isPassengerOnBoard) IncreasePassengerCost(drive.Distance.Length);
        }

        private void IncreasePassengerCost(decimal driveDistance)
        {
            var driveCost = _journeyCostPerKilometer * driveDistance;
            var passengerCost = driveCost / _onBoardPassengerCount;
            _passengerCost += passengerCost;
        }

        public void Visit(PassengerExit exit)
        {
            _onBoardPassengerCount -= 1;
            if (exit.PassengerId.Equals(_passengerId)) _isPassengerOnBoard = false;
        }

        public void Visit(PassengerPickup pickup)
        {
            _onBoardPassengerCount += 1;
            if (pickup.PassengerId.Equals(_passengerId)) _isPassengerOnBoard = true;
        }

        public void Visit(RideStart start)
        {
            _onBoardPassengerCount = 1; // count driver as a passenger
            _isPassengerOnBoard = false;
        }

        public void Visit(RideFinish finish)
        {
        }

        public Money PassengerCost { get { return _passengerCost; } }
    }
}
