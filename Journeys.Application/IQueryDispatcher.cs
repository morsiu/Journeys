using Journeys.Data.Queries;

namespace Journeys.Application
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
