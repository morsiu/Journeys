using System;
using System.Collections.ObjectModel;
using Journeys.Queries.Dtos;

namespace Journeys.Client.Wpf.Features.ShowJourneysByPassengerAndDay
{
    internal class JourneysByDayForPassengerViewModel
    {
        private readonly Lazy<ObservableCollection<JourneysOnDay>> _itemsSource;

        public JourneysByDayForPassengerViewModel(Lazy<ObservableCollection<JourneysOnDay>> itemsSource, string passengerName)
        {
            _itemsSource = itemsSource;
            PassengerName = passengerName;
        }

        public ObservableCollection<JourneysOnDay> Items
        {
            get { return _itemsSource.Value; }
        }

        public string PassengerName { get; private set; }
    }
}
