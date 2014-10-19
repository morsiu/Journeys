using System.Collections.ObjectModel;

namespace Mors.Journeys.Application.Client.Wpf.Components.Notifications
{
    internal sealed class NotifierViewModel
    {
        public NotifierViewModel()
        {
            Items = new ObservableCollection<object>();
        }

        public ObservableCollection<object> Items { get; private set; }

        public void Replace(object notification)
        {
            Items.Clear();
            Items.Add(notification);
        }
    }
}
