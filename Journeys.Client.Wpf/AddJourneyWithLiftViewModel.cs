using System;
using System.Windows.Input;
using Journeys.Client.Wpf.Events;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Command;
using Journeys.Commands;

namespace Journeys.Client.Wpf
{
    internal class AddJourneyWithLiftViewModel
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly EventBus _eventBus;

        public AddJourneyWithLiftViewModel(ICommandDispatcher commandDispatcher, EventBus eventBus)
        {
            _commandDispatcher = commandDispatcher;
            _eventBus = eventBus;
            AddJourneyCommand = new DelegateCommand(AddJourney);
            DateOfJourneyOccurrence = DateTime.Now;
            Notification = new NotifierViewModel();
        }

        public decimal JourneyDistance { get; set; }

        public decimal LiftDistance { get; set; }

        public string PassengerName { get; set; }

        public DateTime DateOfJourneyOccurrence { get; set; }

        public ICommand AddJourneyCommand { get; private set; }

        public NotifierViewModel Notification { get; private set; }
        
        private void AddJourney()
        {
            var personName = PassengerName;
            var journeyId = Guid.NewGuid();
            try
            {
                _commandDispatcher.Dispatch(new AddJourneyWithLiftCommand(journeyId, JourneyDistance, DateOfJourneyOccurrence, personName, LiftDistance));
                _eventBus.Publish(new JourneyAddedEvent(journeyId));
                Notification.Replace(new SuccessNotification("Added successfuly."));
            }
            catch (Exception e)
            {
                Notification.Replace(new ErrorNotification(e.Message));
            }
        }
    }
}
