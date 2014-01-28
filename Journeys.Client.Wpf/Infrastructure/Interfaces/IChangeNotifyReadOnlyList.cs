using System.Collections.Generic;
using System.Collections.Specialized;

namespace Journeys.Client.Wpf.Infrastructure.Interfaces
{
    internal interface IChangeNotifyReadOnlyList<T> : IReadOnlyList<T>, INotifyCollectionChanged
    {
    }
}
