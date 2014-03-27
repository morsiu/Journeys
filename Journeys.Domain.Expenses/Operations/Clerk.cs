using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Policies;
using Journeys.Domain.Infrastructure.Markers;
using System.Collections.Generic;

namespace Journeys.Domain.Expenses.Operations
{
    [Service]
    public sealed class Clerk
    {
        private readonly IJourneyCostCalculator _journeyCost;

        public Clerk(IJourneyCostCalculator journeyCost)
        {
            _journeyCost = journeyCost;
        }

        public ExpenseList CalculateExpenses(IEnumerable<Journey> journeys)
        {
            var expenseList = new ExpenseList();
            foreach (var journey in journeys)
            {
                var journeyExpense = _journeyCost.Calculate(journey);
                expenseList.AddExpense(journeyExpense);
            }
            return expenseList;
        }
    }
}
