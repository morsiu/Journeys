using System.Collections.ObjectModel;

namespace Journeys.Client.Wpf.Infrastructure.Notifications
{
    internal class NotifierViewModel
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
