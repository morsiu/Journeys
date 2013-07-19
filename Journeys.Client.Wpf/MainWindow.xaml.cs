using Journeys.Application;
using Journeys.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
