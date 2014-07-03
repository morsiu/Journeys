using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Test.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Journeys.Domain.Expenses.Test.Capabilities
{
    [TestClass]
    public class ExpenseListTest
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
