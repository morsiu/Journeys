using System.Collections.Generic;
using Journeys.Data.Queries;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Operations;
using Journeys.Domain.Expenses.Policies;

namespace Journeys.Application.Query
{
    internal sealed class PassengerLiftExpensesCalculator
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public PassengerLiftExpensesCalculator(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public Data.Queries.Dtos.PassengerLiftsCost Execute(GetCostOfPassengerLiftsInPeriodQuery query)
        {
            var journeysInPeriod = _queryDispatcher.Dispatch(new GetJourneysInPeriodQuery(query.Period));
            var clerk = new Clerk(new EquallyDistributedCostPolicy(new Money(25m / 100m), query.PassengerId));

            var journeys = BuildJourneys(journeysInPeriod);
            var liftsExpenses = clerk.CalculateExpenses(journeys);

            return new Data.Queries.Dtos.PassengerLiftsCost(liftsExpenses.TotalExpensesValue.Amount);
        }

        private IEnumerable<Journey> BuildJourneys(IEnumerable<Data.Queries.Dtos.Journey> journeysInPeriod)
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
