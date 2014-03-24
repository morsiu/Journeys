using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Infrastructure.Markers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Domain.Expenses.Operations
{
    [Aggregate]
    public sealed class JourneyList
    {
        private readonly Dictionary<IId, Journey> _journeys = new Dictionary<IId, Journey>();

        public void AddJourney(IId journeyId, decimal distance)
        {
            if (_journeys.ContainsKey(journeyId))
            {
                throw new ArgumentException(string.Format("Journey with id {0} has already been added.", journeyId), "journeyId");
            }
            _journeys[journeyId] = new Journey(journeyId, new Point(distance));
        }

        public void AddLift(IId journeyId, IId passengerId, decimal distance)
        {
            if (!_journeys.ContainsKey(journeyId))
            {
                throw new ArgumentException(string.Format("Journey with id {0} has not been added.", journeyId), "journeyId");
            }
            var journey = _journeys[journeyId];
            journey.AddLift(passengerId, new Distance(new Point(), new Point(distance)));
        }

        public ExpenseList GetPassengerLiftExpenses(IId passengerId)
        {
            var journeys = _journeys.Values;
            var liftExpenseList = new ExpenseList();
            foreach (var journey in journeys)
            {
                var liftCost = journey.GetCostFor(passengerId);
                liftExpenseList.AddExpense(new LiftId(journey.Id, passengerId), liftCost);
            }
            return liftExpenseList;
        }
    }
}
