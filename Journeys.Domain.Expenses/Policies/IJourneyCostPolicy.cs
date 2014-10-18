using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Policies
{
    [Policy]
    public interface IJourneyCostPolicy
    {
        Expense CalculateCost(Journey journey);
    }
}
