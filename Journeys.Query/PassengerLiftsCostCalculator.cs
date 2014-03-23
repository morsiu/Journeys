using Journeys.Queries;
using Journeys.Queries.Dtos;
using System;

namespace Journeys.Query
{
    internal class PassengerLiftsCostCalculator
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public PassengerLiftsCostCalculator(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public PassengerLiftsCost Execute(GetCostOfPassengerLiftsInPeriodQuery query)
        {
            return new PassengerLiftsCost(0m);
        }
    }
}
