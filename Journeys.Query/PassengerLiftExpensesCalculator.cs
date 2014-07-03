using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Operations;
using Journeys.Domain.Expenses.Policies;
using Journeys.Data.Queries;
using System.Collections.Generic;
using Dtos = Journeys.Data.Queries.Dtos;

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
            var clerk = new Clerk(new PassengerLiftCostCalculator(new Money(25m / 100m), query.PassengerId));

            var journeys = BuildJourneys(journeysInPeriod);
            var liftsExpenses = clerk.CalculateExpenses(journeys);

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
