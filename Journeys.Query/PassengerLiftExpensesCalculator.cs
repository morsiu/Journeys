using Journeys.Domain.Expenses.Operations;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using System.Collections.Generic;

namespace Journeys.Query
{
    internal class PassengerLiftExpensesCalculator
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public PassengerLiftExpensesCalculator(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public PassengerLiftsCost Execute(GetCostOfPassengerLiftsInPeriodQuery query)
        {
            var journeysInPeriod = _queryDispatcher.Dispatch(new GetJourneysInPeriodQuery(query.Period));

            var journeyList = BuildJourneyList(journeysInPeriod);
            var passengerLiftsCost = journeyList.GetPassengerLiftExpenses(query.PassengerId);

            return new PassengerLiftsCost(passengerLiftsCost.TotalExpense.Amount);
        }

        private JourneyList BuildJourneyList(IEnumerable<Journey> journeysInPeriod)
        {
            var journeyList = new JourneyList();
            foreach (var journey in journeysInPeriod)
            {
                journeyList.AddJourney(journey.Id, journey.RouteDistance);
                foreach (var lift in journey.Lifts)
                {
                    journeyList.AddLift(journey.Id, lift.PassengerId, lift.Distance);
                }
            }
            return journeyList;
        }
    }
}
