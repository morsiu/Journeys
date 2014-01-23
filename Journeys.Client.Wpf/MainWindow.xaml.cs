using Journeys.Client.Wpf.Features.AddJourneysWithLifts;
using Journeys.Client.Wpf.Features.ShowJourneysByPassengerAndDay;
using Journeys.Client.Wpf.Features.ShowJourneysInCalendar;

namespace Journeys.Client.Wpf
{
    internal partial class MainWindow
    {
        public MainWindow(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            IEventBus eventBus,
            IIdFactory idFactory)
        {
            InitializeComponent();
            AddJourney.DataContext = new AddJourneyWithLiftViewModel(commandDispatcher, queryDispatcher, eventBus, idFactory);
            var journeyCalendarsViewModel = new JourneyCalendarsViewModel(queryDispatcher, eventBus);
            journeyCalendarsViewModel.Refresh();
            JourneyCalendars.DataContext = journeyCalendarsViewModel;
        }
    }
}
