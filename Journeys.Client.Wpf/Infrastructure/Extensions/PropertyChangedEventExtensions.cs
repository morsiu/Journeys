using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Journeys.Client.Wpf.Infrastructure.Extensions
{
    internal static class PropertyChangedEventExtensions
    {
        public static void Raise(this PropertyChangedEventHandler handler, [CallerMemberName] string propertyName = null)
        {
            if (handler == null || propertyName == null) return;
            handler(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
