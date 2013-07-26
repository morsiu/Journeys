using Journeys.Application;
using Journeys.Client.Wpf.Events;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Journeys.Client.Wpf
{
    internal class AddJourneyViewModel : INotifyPropertyChanged
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly EventBus _eventBus;
        private string _notification;

        public AddJourneyViewModel(ICommandDispatcher commandDispatcher, EventBus eventBus)
        {
            _commandDispatcher = commandDispatcher;
            _eventBus = eventBus;
            AddJourneyCommand = new Command(AddJourney);
            DateOfJourneyOccurrence = DateTime.Now;
        }

        public decimal JourneyDistance { get; set; }

        public decimal LiftDistance { get; set; }

        public string PassengerName { get; set; }

        public DateTime DateOfJourneyOccurrence { get; set; }

        public ICommand AddJourneyCommand { get; private set; }

        public string Notification
        {
            get { return _notification; }
            set
            {
                if (value == _notification) return;
                _notification = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void AddJourney()
        {
            var personName = PassengerName;
            var journeyId = Guid.NewGuid();
            try
            {
                _commandDispatcher.Dispatch(new AddJourneyCommand(journeyId, JourneyDistance, DateOfJourneyOccurrence, personName, LiftDistance));
                _eventBus.Publish(new JourneyAddedEvent(journeyId));
                Notification = "Added successfuly";
            }
            catch (Exception e)
            {
                Notification = e.Message;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
