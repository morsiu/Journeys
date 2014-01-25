using System;
using System.Linq;
using System.Windows.Input;
using Journeys.Client.Wpf.Events;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Client.Wpf.Infrastructure.Notifications;
using Journeys.Commands;
using System.Collections.Generic;
using Journeys.Queries;
using Journeys.Commands.Dtos;

namespace Journeys.Client.Wpf.Features.AddJourneysWithLifts
{
    internal class AddJourneyWithLiftViewModel
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IEventBus _eventBus;
        private readonly IIdFactory _idFactory;

        public AddJourneyWithLiftViewModel(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IEventBus eventBus, IIdFactory idFactory)
        {
            _commandDispatcher = commandDispatcher;
            _eventBus = eventBus;
            _idFactory = idFactory;
            AddJourneyCommand = new DelegateCommand(AddJourney);
            DateOfOccurrence = DateTime.Now.Date;
            Notification = new NotifierViewModel();
            try
            {
                PassengerNames = queryDispatcher.Dispatch(new GetPeopleNamesQuery()).Select(personName => personName.Name).ToList();
            }
            catch
            {
                PassengerNames = new List<string>();
            }
        }

        public decimal RouteDistance { get; set; }

        public decimal LiftDistance { get; set; }

        public string PassengerName { get; set; }

        public List<string> PassengerNames { get; private set; }

        public DateTime DateOfOccurrence { get; set; }

        public ICommand AddJourneyCommand { get; private set; }

        public NotifierViewModel Notification { get; private set; }
        
        private void AddJourney()
        {
            var personName = PassengerName;
            var journeyId = _idFactory.Create();
            try
            {
                var lifts = new[] { new Lift(PassengerName, LiftDistance) };
                _commandDispatcher.Dispatch(new AddJourneyWithLiftsCommand(journeyId, RouteDistance, DateOfOccurrence, lifts));
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
