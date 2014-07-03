using Journeys.Data.Queries;

namespace Journeys.Application.Command
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
