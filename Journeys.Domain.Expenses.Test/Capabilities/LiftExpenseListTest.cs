using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Test.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Journeys.Domain.Expenses.Test.Capabilities
{
    [TestClass]
    public class LiftExpenseListTest
    {
        public static IId JourneyId = new Id(0);
        public static IId PersonId = new Id(1);
    }
}
