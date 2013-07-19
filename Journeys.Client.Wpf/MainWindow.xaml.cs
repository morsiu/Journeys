using Journeys.Application;
using Journeys.Application.Commands;
using System;
using System.Windows;

namespace Journeys.Client.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly Guid PersonId = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        private readonly ICommandDispatcher _commandDispatcher;

        public MainWindow(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            InitializeComponent();
        }

        private void AddJourney_Click(object sender, RoutedEventArgs e)
        {
            var journeyDateOfOccurence = DateTime.Now;
            var journeyDistance = int.Parse(JourneyDistanceField.Text);
            var liftDistance = int.Parse(LiftDistanceField.Text);
            _commandDispatcher.Dispatch(new AddJourneyCommand(journeyDistance, journeyDateOfOccurence, PersonId, liftDistance));
        }
    }
}
