using Mors.AppPlatform.Common;

namespace Mors.Journeys.Application.Client.Wpf
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
