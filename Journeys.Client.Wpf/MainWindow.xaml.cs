using Journeys.Client.Wpf.Features.AddingJourneysWithLifts;
using Journeys.Client.Wpf.Features.ShowJourneysByPassengerAndDay;
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
            AddJourney.DataContext = new AddJourneyWithLiftViewModel(commandDispatcher, eventBus);
            var journeysByPassengerThenDayViewModel = new JourneysByPassengerThenDayViewModel(queryDispatcher, eventBus);
            journeysByPassengerThenDayViewModel.Reload();
            JourneysByPassengerThenDay.DataContext = journeysByPassengerThenDayViewModel;
        }        
    }
}
