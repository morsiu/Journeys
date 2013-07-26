using Journeys.Application;
using Journeys.Application.Commands;
using Journeys.Client.Wpf.Events;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Commands;
using Journeys.Data;
using Journeys.Data.Journeys;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Journeys.Client.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly EventBus _eventBus;
        private readonly ObservableCollection<JourneyWithLift> _journeysWithLifts;

        internal MainWindow(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, EventBus eventBus)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _eventBus = eventBus;
            _eventBus.Subscribe<JourneyAddedEvent>(HandleEvent);
            _journeysWithLifts = new ObservableCollection<JourneyWithLift>();
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            AddJourney.DataContext = new AddJourneyViewModel(_commandDispatcher, _eventBus);
            JourneysWithLiftsList.ItemsSource = _journeysWithLifts;
            LoadJourneysWithLifts();
        }

        private void LoadJourneysWithLifts()
        {
            var journeysWithLifts = _queryDispatcher.Dispatch(new GetAllJourneysWithLiftsQuery());
            foreach (var item in journeysWithLifts)
            {
                _journeysWithLifts.Add(item);
            }
        }

        private void HandleEvent(JourneyAddedEvent @event)
        {
            var newJourneysWithLifts = _queryDispatcher.Dispatch(new GetJourneysWithLiftsByJourneyIdQuery(@event.JourneyId));
            foreach (var item in newJourneysWithLifts)
            {
                _journeysWithLifts.Add(item);
            }
        }
    }
}
