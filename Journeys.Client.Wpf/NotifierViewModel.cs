using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Client.Wpf
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
