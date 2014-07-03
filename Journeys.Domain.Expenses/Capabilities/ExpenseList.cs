using Journeys.Common;
using System.Collections.Generic;

namespace Journeys.Domain.Expenses.Capabilities
{
    public sealed class ExpenseList
    {
        private readonly Dictionary<object, Money> _expenseValues = new Dictionary<object, Money>();

        public Money TotalExpensesValue { get; private set; }

        public void AddExpense(Expense expense)
        {
            var previousExpense = GetExpense(expense.SubjectId);
            StoreExpense(previousExpense.Increase(expense.Value));
            IncreaseTotalExpensesValue(expense.Value);
        }

        public Expense GetExpense(object subjectId)
        {
            Money expenseValue;
            _expenseValues.TryGetValue(subjectId, out expenseValue);
            return new Expense(subjectId, expenseValue);
        }
        
        private void StoreExpense(Expense expense)
        {
            _expenseValues[expense.SubjectId] = expense.Value;
        }

        private void IncreaseTotalExpensesValue(Money expense)
        {
            TotalExpensesValue += expense;
        }
    }
}
