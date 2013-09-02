using System;
using System.Windows.Input;
using Journeys.Client.Wpf.Events;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Client.Wpf.Infrastructure.Notifications;
using Journeys.Commands;

namespace Journeys.Client.Wpf.Features.AddJourneysWithLifts
{
    internal class AddJourneyWithLiftViewModel
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IEventBus _eventBus;
        private readonly IIdFactory _idFactory;

        public AddJourneyWithLiftViewModel(ICommandDispatcher commandDispatcher, IEventBus eventBus, IIdFactory idFactory)
        {
            _commandDispatcher = commandDispatcher;
            _eventBus = eventBus;
            _idFactory = idFactory;
            AddJourneyCommand = new DelegateCommand(AddJourney);
            DateOfOccurrence = DateTime.Now.Date;
            Notification = new NotifierViewModel();
        }

        public decimal RouteDistance { get; set; }

        public decimal LiftDistance { get; set; }

        public string PassengerName { get; set; }

        public DateTime DateOfOccurrence { get; set; }

        public ICommand AddJourneyCommand { get; private set; }

        public NotifierViewModel Notification { get; private set; }
        
        private void AddJourney()
        {
            var personName = PassengerName;
            var journeyId = _idFactory.Create();
            try
            {
                _commandDispatcher.Dispatch(new AddJourneyWithLiftCommand(journeyId, RouteDistance, DateOfOccurrence, personName, LiftDistance));
                _eventBus.Publish(new JourneyWithLiftAddedEvent(journeyId));
                Notification.Replace(new SuccessNotification("Added successfuly."));
            }
            catch (Exception e)
            {
                Notification.Replace(new ErrorNotification(e.Message));
            }
        }
    }
}
