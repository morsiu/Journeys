using System.Collections.Generic;
using Mors.AppPlatform.Common.Services;
using Mors.Journeys.Data.Queries;
using Mors.Journeys.Data.Queries.Dtos;
using Mors.Journeys.Domain.Expenses.Capabilities;
using Mors.Journeys.Domain.Expenses.Operations;
using Mors.Journeys.Domain.Expenses.Policies;

namespace Mors.Journeys.Application.QueryHandlers
{
    internal sealed class PassengerLiftExpensesCalculator
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public PassengerLiftExpensesCalculator(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public PassengerLiftsCost Execute(GetCostOfPassengerLiftsInPeriodQuery query)
        {
            var journeysInPeriod = _queryDispatcher.Dispatch(new GetJourneysInPeriodQuery(query.Period));
            var clerk = new Clerk(new EquallyDistributedCostPolicy(new Money(25m / 100m), query.PassengerId));

            var journeys = BuildJourneys(journeysInPeriod);
            var liftsExpenses = clerk.CalculateExpenses(journeys);

            return new PassengerLiftsCost(liftsExpenses.TotalExpensesValue.Amount);
        }

        private static IEnumerable<Domain.Expenses.Capabilities.Journey> BuildJourneys(
            IEnumerable<Data.Queries.Dtos.Journey> journeysInPeriod)
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
