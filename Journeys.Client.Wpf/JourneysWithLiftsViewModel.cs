using System.Collections.ObjectModel;
using Journeys.Client.Wpf.Events;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using Journeys.Query;

namespace Journeys.Client.Wpf
{
    internal class JourneysWithLiftsViewModel
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public JourneysWithLiftsViewModel(EventBus eventBus, IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            Items = new ObservableCollection<JourneyWithLift>();
            eventBus.Subscribe<JourneyWithLiftAddedEvent>(HandleEvent);
        }

        public ObservableCollection<JourneyWithLift> Items { get; private set; }

        public void Reload()
        {
            var newItems = _queryDispatcher.Dispatch(new GetAllJourneysWithLiftsQuery());
            Items.Clear();
            foreach (var item in newItems)
            {
                Items.Add(item);
            }
        }

        private void HandleEvent(JourneyWithLiftAddedEvent @event)
        {
            var newItems = _queryDispatcher.Dispatch(new GetJourneysWithLiftsByJourneyIdQuery(@event.JourneyId));
            foreach (var item in newItems)
            {
                Items.Add(item);
            }
        }
    }
}
