using System;

namespace Journeys.Client.Wpf.Infrastructure.Extensions
{
    internal static class EventExtensions
    {
        public static void Raise(this EventHandler handler, object sender)
        {
            var copy = handler;
            if (copy != null)
            {
                copy(sender, EventArgs.Empty);
            }
        }
    }
}
