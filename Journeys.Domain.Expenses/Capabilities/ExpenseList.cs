using Journeys.Common;
using System.Collections.Generic;

namespace Journeys.Domain.Expenses.Capabilities
{
    public sealed class ExpenseList
    {
        private readonly Dictionary<IId, Money> _expenses = new Dictionary<IId, Money>();

        public Money TotalExpense { get; private set; }

        public void AddExpense(IId expenseId, Money expense)
        {
            var previousExpense = GetExpense(expenseId);
            SetExpense(expenseId, previousExpense + expense);
            IncreaseTotalExpense(expense);
        }

        public Money GetExpense(IId expenseId)
        {
            Money expense;
            _expenses.TryGetValue(expenseId, out expense);
            return expense;
        }
        
        private void SetExpense(IId expenseId, Money expense)
        {
            _expenses[expenseId] = expense;
        }

        private void IncreaseTotalExpense(Money expense)
        {
            TotalExpense += expense;
        }
    }
}
