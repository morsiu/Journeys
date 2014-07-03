using Journeys.Data.Queries;

namespace Journeys.Application.Client.Wpf
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
