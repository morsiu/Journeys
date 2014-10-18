using Mors.Journeys.Domain.Expenses.Capabilities;
using Mors.Journeys.Domain.Infrastructure.Markers;

namespace Mors.Journeys.Domain.Expenses.Policies
{
    [Policy]
    public interface IJourneyCostPolicy
    {
        Expense CalculateCost(Journey journey);
    }
}
