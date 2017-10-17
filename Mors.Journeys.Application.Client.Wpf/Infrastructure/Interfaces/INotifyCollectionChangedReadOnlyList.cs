using System.Collections.Generic;
using System.Collections.Specialized;

namespace Mors.Journeys.Application.Client.Wpf.Infrastructure.Interfaces
{
    internal interface INotifyCollectionChangedReadOnlyList<T> : IReadOnlyList<T>, INotifyCollectionChanged
    {
    }
}
