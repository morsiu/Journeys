using Mors.Journeys.Data;

namespace Mors.Journeys.Application
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
