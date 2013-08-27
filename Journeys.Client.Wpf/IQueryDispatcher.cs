using Journeys.Queries;

namespace Journeys.Client.Wpf
{
    internal interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
