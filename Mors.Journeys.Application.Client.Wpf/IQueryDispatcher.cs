using Mors.Journeys.Data;

namespace Mors.Journeys.Application.Client.Wpf
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
