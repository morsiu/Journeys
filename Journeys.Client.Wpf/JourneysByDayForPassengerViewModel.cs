using System;
using System.Collections.ObjectModel;
using Journeys.Queries.Dtos;

namespace Journeys.Client.Wpf
{
    internal class JourneysByDayForPassengerViewModel
    {
        private readonly Lazy<ObservableCollection<JourneysByDay>> _itemsSource;

        public JourneysByDayForPassengerViewModel(Lazy<ObservableCollection<JourneysByDay>> itemsSource, string passengerName)
        {
            _itemsSource = itemsSource;
            PassengerName = passengerName;
        }

        public ObservableCollection<JourneysByDay> Items
        {
            get { return _itemsSource.Value; }
        }

        public string PassengerName { get; private set; }
    }
}
