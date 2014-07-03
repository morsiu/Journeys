using Journeys.Data.Queries;

namespace Journeys.Application.EventSourcing
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
