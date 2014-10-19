using Mors.Journeys.Application.Client.Wpf.Features.AddJourneysWithLifts;
using Mors.Journeys.Application.Client.Wpf.Features.CalculatePassengerLiftsCostInPeriod;
using Mors.Journeys.Application.Client.Wpf.Features.ShowJourneysInCalendar;

namespace Mors.Journeys.Application.Client.Wpf
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
            AddJourney.DataContext = new AddJourneyWithLiftsViewModel(commandDispatcher, queryDispatcher, eventBus, idFactory);
            var journeyCalendarsViewModel = new JourneyCalendarsViewModel(queryDispatcher, eventBus);
            journeyCalendarsViewModel.Refresh();
            JourneyCalendars.DataContext = journeyCalendarsViewModel;
            var calculatePassengerLiftsCostInPeriod = new CalculatePassengerLiftsCostInPeriodViewModel(queryDispatcher);
            calculatePassengerLiftsCostInPeriod.Refresh();
            CalculatePassengerLiftsCostInPeriod.DataContext = calculatePassengerLiftsCostInPeriod;
        }
    }
}
