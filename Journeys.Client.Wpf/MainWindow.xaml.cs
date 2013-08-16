using Journeys.Client.Wpf.Infrastructure;
using Journeys.Command;
using Journeys.Query;

namespace Journeys.Client.Wpf
{
    public partial class MainWindow
    {
        internal MainWindow(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, EventBus eventBus)
        {
            InitializeComponent();
            AddJourney.DataContext = new AddJourneyViewModel(commandDispatcher, eventBus);
            var journeysViewModel = new JourneysViewModel(eventBus, queryDispatcher);
            Journeys.DataContext = journeysViewModel;
            journeysViewModel.Reload();
        }        
    }
}
