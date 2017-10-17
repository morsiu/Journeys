using System.Collections.Specialized;

namespace Mors.Journeys.Application.Client.Wpf.Infrastructure.Extensions
{
    internal static class NotifyCollectionChangedEventExtensions
    {
        public static void Raise(this NotifyCollectionChangedEventHandler handler, object sender, NotifyCollectionChangedEventArgs args)
        {
            if (handler == null || args == null) return;
            handler(sender, args);
        }
    }
}
