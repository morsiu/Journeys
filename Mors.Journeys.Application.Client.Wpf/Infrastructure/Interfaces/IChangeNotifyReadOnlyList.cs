using System.Collections.Generic;
using System.Collections.Specialized;

namespace Mors.Journeys.Application.Client.Wpf.Infrastructure.Interfaces
{
    internal interface IChangeNotifyReadOnlyList<T> : IReadOnlyList<T>, INotifyCollectionChanged
    {
    }
}
