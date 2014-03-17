using Journeys.Common;
using System.Collections.Generic;

namespace Journeys.Domain.Expenses.Capabilities
{
    internal sealed class ExpenseList
    {
        private readonly Dictionary<IId, Money> _expenses = new Dictionary<IId, Money>();

        public void AddExpense(IId expenseId, Money expense)
        {
            var previousExpense = GetExpense(expenseId);
            _expenses[expenseId] = previousExpense + expense;
        }

        public Money GetExpense(IId expenseId)
        {
            Money expense;
            _expenses.TryGetValue(expenseId, out expense);
            return expense;
        }
    }
}
