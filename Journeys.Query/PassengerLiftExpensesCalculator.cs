using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Operations;
using Journeys.Queries;
using System.Collections.Generic;
using Dtos = Journeys.Queries.Dtos;

namespace Journeys.Query
{
    internal class PassengerLiftExpensesCalculator
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public PassengerLiftExpensesCalculator(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public Dtos.PassengerLiftsCost Execute(GetCostOfPassengerLiftsInPeriodQuery query)
        {
            var journeysInPeriod = _queryDispatcher.Dispatch(new GetJourneysInPeriodQuery(query.Period));

            var journeys = BuildJourneys(journeysInPeriod);
            var liftsExpenses = new ExpenseList();
            foreach (var journey in journeys)
            {
                var liftExpense = journey.GetCostFor(query.PassengerId);
                liftsExpenses.AddExpense(liftExpense);
            }

            return new Dtos.PassengerLiftsCost(liftsExpenses.TotalExpensesValue.Amount);
        }

        private IEnumerable<Journey> BuildJourneys(IEnumerable<Dtos.Journey> journeysInPeriod)
        {
            var journeyFactory = new JourneyFactory();
            foreach (var journey in journeysInPeriod)
            {
                var journeyBuilder = journeyFactory.Create(journey.Id, journey.RouteDistance);
                foreach (var lift in journey.Lifts)
                {
                    journeyBuilder.AddLift(lift.PassengerId, lift.Distance);
                }
                yield return journeyBuilder.Build();
            }
        }
    }
}
