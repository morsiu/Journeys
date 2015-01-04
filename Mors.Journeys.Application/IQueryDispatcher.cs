using Mors.Journeys.Common;

namespace Mors.Journeys.Application
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
