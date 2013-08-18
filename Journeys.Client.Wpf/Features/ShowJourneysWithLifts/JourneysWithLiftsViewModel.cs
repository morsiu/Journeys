using System.Collections.ObjectModel;
using Journeys.Client.Wpf.Events;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using Journeys.Query;

namespace Journeys.Client.Wpf.Features.ShowJourneysWithLifts
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

        public void Refresh()
        {
            var newItems = _queryDispatcher.Dispatch(new GetJourneysWithLiftsQuery());
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
