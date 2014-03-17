using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Test.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Journeys.Domain.Expenses.Test.Capabilities
{
    [TestClass]
    public class ExpenseListTest
    {
        private static IId Id = new Id(1);

        [TestMethod]
        public void GetExpenseShouldReturnExpenseAfterItWasAddedWithAddExpenseUsingSameId()
        {
            var list = new ExpenseList();
            var expense = new Money(10m);

            list.AddExpense(Id, expense);
            var result = list.GetExpense(Id);

            Assert.AreEqual(expense, result);
        }

        [TestMethod]
        public void AddExpenseShouldIncreaseExpenseAfterItWasAlreadyAdded()
        {
            var list = new ExpenseList();
            var expense = new Money(10m);

            list.AddExpense(Id, expense);
            list.AddExpense(Id, expense);
            var updatedExpense = list.GetExpense(Id);

            Assert.AreEqual(new Money(20m), updatedExpense);
        }

        [TestMethod]
        public void GetExpenseShouldReturnZeroExpenseAfterConstruction()
        {
            var list = new ExpenseList();
            var zeroExpense = new Money();

            var result = list.GetExpense(Id);

            Assert.AreEqual(zeroExpense, result);
        }

        public void TotalExpenseShouldBeZeroAfterConstruction()
        {
            var list = new ExpenseList();

            var total = list.TotalExpense;

            Assert.AreEqual(new Money(), total);
        }

        [TestMethod]
        public void AddExpenseShouldIncreaseTotalWithTheExpense()
        {
            var list = new ExpenseList();
            var expense = new Money(10m);

            list.AddExpense(Id, expense);
            var totalExpense = list.TotalExpense;

            Assert.AreEqual(expense, totalExpense);
        }
    }
}
