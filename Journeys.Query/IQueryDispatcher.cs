using Journeys.Data.Queries;

namespace Journeys.Query
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
