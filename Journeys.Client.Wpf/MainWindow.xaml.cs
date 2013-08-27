using Journeys.Client.Wpf.Features.AddJourneysWithLifts;
using Journeys.Client.Wpf.Features.ShowJourneysByPassengerAndDay;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Dispatching;
using Journeys.Repositories;

namespace Journeys.Client.Wpf
{
    public partial class MainWindow
    {
        internal MainWindow(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, EventBus eventBus, IdFactory idFactory)
        {
            InitializeComponent();
            AddJourney.DataContext = new AddJourneyWithLiftViewModel(commandDispatcher, eventBus, idFactory);
            var journeysByPassengerThenDayViewModel = new JourneysByPassengerThenDayViewModel(queryDispatcher, eventBus);
            journeysByPassengerThenDayViewModel.Reload();
            JourneysByPassengerThenDay.DataContext = journeysByPassengerThenDayViewModel;
        }        
    }
}
