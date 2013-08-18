using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Journeys.Client.Wpf.Events;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using Journeys.Query;

namespace Journeys.Client.Wpf
{
    internal class JourneysByPassengerThenDayViewModel
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public JourneysByPassengerThenDayViewModel(IQueryDispatcher queryDispatcher, EventBus eventBus)
        {
            _queryDispatcher = queryDispatcher;
            eventBus.Subscribe<JourneyWithLiftAddedEvent>(Handle);
            Items = new ObservableCollection<JourneysByDayForPassengerViewModel>();
            PeriodEnd = DateTime.Now;
            PeriodStart = PeriodEnd.AddMonths(-1);
            RefreshCommand = new DelegateCommand(Reload);
        }

        public ICommand RefreshCommand { get; private set; }

        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }

        public ObservableCollection<JourneysByDayForPassengerViewModel> Items { get; private set; }

        public void Reload()
        {
            var peopleNames = _queryDispatcher.Dispatch(new GetPeopleNamesQuery());
            Items.Clear();
            foreach (var passenger in peopleNames)
            {
                var itemsSource = new Lazy<ObservableCollection<JourneysByDay>>(() => LoadJourneysByDayForPassenger(passenger.Id));
                Items.Add(new JourneysByDayForPassengerViewModel(itemsSource, passenger.Name));
            }
        }

        private ObservableCollection<JourneysByDay> LoadJourneysByDayForPassenger(Guid passengerId)
        {
            var items = _queryDispatcher.Dispatch(new GetJourneysByDayForPassengerInPeriodQuery(passengerId, PeriodStart, PeriodEnd));
            return new ObservableCollection<JourneysByDay>(items);
        }

        private void Handle(JourneyWithLiftAddedEvent @event)
        {
            Reload();
        }
    }
}
