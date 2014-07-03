using Journeys.Data.Queries;

namespace Journeys.Application.Query
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
