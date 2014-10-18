using System.Collections.Generic;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Policies;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Operations
{
    [Service]
    public sealed class Clerk
    {
        private readonly IJourneyCostPolicy _journeyCostPolicy;

        public Clerk(IJourneyCostPolicy journeyCostPolicy)
        {
            _journeyCostPolicy = journeyCostPolicy;
        }

        public ExpenseList CalculateExpenses(IEnumerable<Journey> journeys)
        {
            var expenseList = new ExpenseList();
            foreach (var journey in journeys)
            {
                var journeyExpense = _journeyCostPolicy.CalculateCost(journey);
                expenseList.AddExpense(journeyExpense);
            }
            return expenseList;
        }
    }
}
