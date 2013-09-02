using Journeys.Client.Wpf.Features.AddJourneysWithLifts;
using Journeys.Client.Wpf.Features.ShowJourneysByPassengerAndDay;

namespace Journeys.Client.Wpf
{
    internal partial class MainWindow
    {
        internal MainWindow(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IEventBus eventBus, IIdFactory idFactory)
        {
            InitializeComponent();
            AddJourney.DataContext = new AddJourneyWithLiftViewModel(commandDispatcher, eventBus, idFactory);
            var journeysByPassengerThenDayViewModel = new JourneysByPassengerThenDayViewModel(queryDispatcher, eventBus);
            journeysByPassengerThenDayViewModel.Reload();
            JourneysByPassengerThenDay.DataContext = journeysByPassengerThenDayViewModel;
        }        
    }
}
