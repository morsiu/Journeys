using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mors.Journeys.Domain.Expenses.Capabilities;
using Mors.Journeys.Domain.Test;

namespace Mors.Journeys.Domain.Expenses.Test.Capabilities
{
    [TestClass]
    public sealed class ExpenseListTest
    {
        private static object Id = new Id(1);

        [TestMethod]
        public void GetExpenseShouldReturnExpenseAfterItWasAddedWithAddExpenseUsingSameId()
        {
            var list = new ExpenseList();
            var expenseValue = new Money(10m);

            list.AddExpense(new Expense(Id, expenseValue));
            var result = list.GetExpense(Id);

            Assert.AreEqual(expenseValue, result.Value);
        }

        [TestMethod]
        public void AddExpenseShouldIncreaseExpenseAfterItWasAlreadyAdded()
        {
            var list = new ExpenseList();
            var expenseValue = new Money(10m);

            list.AddExpense(new Expense(Id, expenseValue));
            list.AddExpense(new Expense(Id, expenseValue));
            var updatedExpense = list.GetExpense(Id);

            Assert.AreEqual(new Money(20m), updatedExpense.Value);
        }

        [TestMethod]
        public void GetExpenseShouldReturnZeroExpenseAfterConstruction()
        {
            var list = new ExpenseList();

            var result = list.GetExpense(Id);

            Assert.AreEqual(new Money(), result.Value);
        }

        [TestMethod]
        public void TotalExpenseShouldBeZeroAfterConstruction()
        {
            var list = new ExpenseList();

            var total = list.TotalExpensesValue;

            Assert.AreEqual(new Money(), total);
        }

        [TestMethod]
        public void AddExpenseShouldIncreaseTotalWithTheExpense()
        {
            var list = new ExpenseList();
            var expense = new Money(10m);

            list.AddExpense(new Expense(Id, expense));
            var totalExpense = list.TotalExpensesValue;

            Assert.AreEqual(expense, totalExpense);
        }
    }
}
