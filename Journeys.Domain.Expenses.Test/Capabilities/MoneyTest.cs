using Journeys.Domain.Expenses.Capabilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Journeys.Domain.Expenses.Test.Capabilities
{
    [TestClass]
    public class MoneyTest
    {
        [TestMethod]
        public void ShouldHaveAmountGivenAtConstruction()
        {
            var money = new Money(10m);

            Assert.AreEqual(10, money.Amount);
        }

        [TestMethod]
        public void ShouldHaveZeroAmountAfterDefaultConstruction()
        {
            var money = new Money();

            Assert.AreEqual(0, money.Amount);
        }

        [TestMethod]
        public void AddingMoneyShouldResultInMoneyWithAmountBeingSumOfAddentsAmounts()
        {
            var a = new Money(10m);
            var b = new Money(20m);

            var c = a + b;

            Assert.AreEqual(30, c.Amount);
        }

        [TestMethod]
        public void MultiplyingMoneyByNumberShouldResultInMoneyWithAmountMultipliedByThatNumber()
        {
            var a = new Money(10m);

            var c = a * 3;

            Assert.AreEqual(30, c.Amount);
        }

        [TestMethod]
        public void MultiplyingNumberByMoneyShouldResultInMoneyWithAmountMultipliedByThatNumber()
        {
            var a = new Money(10m);

            var c = 3 * a;

            Assert.AreEqual(30, c.Amount);
        }
    }
}
