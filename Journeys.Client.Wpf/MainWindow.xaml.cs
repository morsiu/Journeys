using Journeys.Application;
using Journeys.Application.Commands;
using Journeys.Client.Wpf.Events;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Commands;
using Journeys.Data;
using Journeys.Data.Journeys;
using Journeys.Queries;
using System;
using System.Windows;

namespace Journeys.Client.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly EventBus _eventBus;

        internal MainWindow(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, EventBus eventBus)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _eventBus = eventBus;
            _eventBus.Subscribe<JourneyAddedEvent>(HandleEvent);
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            AddJourney.DataContext = new AddJourneyViewModel(_commandDispatcher, _eventBus);
            LoadJourneysWithLifts();
        }

        private void LoadJourneysWithLifts()
        {
            var journeysWithLifts = _queryDispatcher.Dispatch(new GetAllJourneysWithLiftsQuery());
            JourneysWithLiftsList.ItemsSource = journeysWithLifts;
        }

        private void HandleEvent(JourneyAddedEvent @event)
        {
            LoadJourneysWithLifts();
        }
    }
}
