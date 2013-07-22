using Journeys.Application;
using Journeys.Application.Commands;
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
        private readonly Guid PersonId = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public MainWindow(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            LoadJourneysWithLifts();
        }

        private void LoadJourneysWithLifts()
        {
            var journeysWithLifts = _queryDispatcher.Dispatch(new GetAllJourneysWithLiftsQuery());
            JourneysWithLiftsList.ItemsSource = journeysWithLifts;
        }

        private void AddJourney_Click(object sender, RoutedEventArgs e)
        {
            var journeyDateOfOccurence = DateTime.Now;
            var journeyDistance = int.Parse(JourneyDistanceField.Text);
            var liftDistance = int.Parse(LiftDistanceField.Text);
            var journeyId = Guid.NewGuid();
            _commandDispatcher.Dispatch(new AddJourneyCommand(journeyId, journeyDistance, journeyDateOfOccurence, PersonId, liftDistance));
            LoadJourneysWithLifts();
        }
    }
}
