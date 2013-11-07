using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Client.Wpf.Infrastructure.Extensions
{
    internal static class EventExtensions
    {
        public static void Raise(this EventHandler handler)
        {
            var copy = handler;
            if (copy != null)
            {
                copy(null, EventArgs.Empty);
            }
        }
    }
}
