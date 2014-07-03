using Journeys.Client.Wpf.Commands;
using Journeys.Client.Wpf.Components.Notifications;
using Journeys.Client.Wpf.Events;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Client.Wpf.Infrastructure.Extensions;
using Journeys.Client.Wpf.Queries;
using Journeys.Client.Wpf.Settings;
using Journeys.Data.Commands;
using Journeys.Data.Queries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Journeys.Client.Wpf.Features.AddJourneysWithLifts
{
    internal class AddJourneyWithLiftsViewModel : INotifyPropertyChanged
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IEventBus _eventBus;
        private readonly IIdFactory _idFactory;
        private decimal _routeDistance;
        private DateTime _dateOfOccurrence;

        public AddJourneyWithLiftsViewModel(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IEventBus eventBus, IIdFactory idFactory)
        {
            _commandDispatcher = commandDispatcher;
            _eventBus = eventBus;
            _idFactory = idFactory;
            AddJourneyCommand = new DelegateCommand(AddJourney);
            LoadSettingCommand = new DelegateCommand<string>(LoadSetting);
            SaveSettingCommand = new DelegateCommand<string>(SaveSetting);
            RemoveSettingCommand = new DelegateCommand<string>(RemoveSetting);
            DateOfOccurrence = DateTime.Now.Date;
            Notification = new NotifierViewModel();
            Lifts = new ObservableCollection<LiftViewModel>();
            try
            {
                PassengerNames = queryDispatcher.Dispatch(new GetPeopleNamesQuery()).Select(personName => personName.Name).ToList();
            }
            catch
            {
                PassengerNames = new List<string>();
            }
            try
            {
                JourneyTemplates = new ObservableCollection<JourneyTemplate>(queryDispatcher.Dispatch(new GetJourneyTemplatesQuery()));
            }
            catch
            {
                JourneyTemplates = new ObservableCollection<JourneyTemplate>();
            }
        }

        public decimal RouteDistance
        {
            get { return _routeDistance; }
            set { _routeDistance = value; PropertyChanged.Raise(this); }
        }

        public ObservableCollection<LiftViewModel> Lifts { get; private set; }

        public List<string> PassengerNames { get; private set; }

        public DateTime DateOfOccurrence
        {
            get { return _dateOfOccurrence; }
            set { _dateOfOccurrence = value; PropertyChanged.Raise(this); }
        }

        public ICommand AddJourneyCommand { get; private set; }

        public ICommand LoadSettingCommand { get; set; }

        public ICommand SaveSettingCommand { get; set; }

        public ICommand RemoveSettingCommand { get; set; }

        public NotifierViewModel Notification { get; private set; }

        public ObservableCollection<JourneyTemplate> JourneyTemplates { get; private set; }
        
        private void AddJourney()
        {
            var journeyId = _idFactory.Create();
            try
            {
                var lifts = Lifts.Select(lift => lift.ToDto());
                _commandDispatcher.Dispatch(new AddJourneyWithLiftsCommand(journeyId, RouteDistance, DateOfOccurrence, lifts));
                _eventBus.Publish(new JourneyWithLiftsAddedEvent(journeyId));
                Notification.Replace(new SuccessNotification("Added successfuly."));
            }
            catch (Exception e)
            {
                Notification.Replace(new ErrorNotification(e.Message));
            }
        }

        private void LoadSetting(string journeyTemplateName)
        {
            var journeyTemplate = JourneyTemplates.FirstOrDefault(t => t.Name == journeyTemplateName);
            if (journeyTemplate != null)
            {
                Fill(journeyTemplate);
            }
        }

        private void Fill(JourneyTemplate journeyTemplate)
        {
            RouteDistance = journeyTemplate.RouteDistance;
            Lifts.Clear();
            foreach (var liftTemplate in journeyTemplate.Lifts)
            {
                Lifts.Add(new LiftViewModel { LiftDistance = liftTemplate.LiftDistance, PassengerName = liftTemplate.PassengerName });
            }
        }

        private void SaveSetting(string journeyTemplateName)
        {
            var newJourneyTemplate = Gather(journeyTemplateName);
            try
            {
                _commandDispatcher.Dispatch(new StoreJourneyTemplatesCommand(new[] { newJourneyTemplate }, Enumerable.Empty<string>()));
                var existingTemplate = JourneyTemplates.FirstOrDefault(t => t.Name == journeyTemplateName);
                JourneyTemplates.Remove(existingTemplate);
                JourneyTemplates.Add(newJourneyTemplate);
            }
            catch (Exception e)
            {
                Notification.Replace(new ErrorNotification(e.Message));
            }
        }

        private JourneyTemplate Gather(string journeyTemplateName)
        {
            return new JourneyTemplate
            {
                Name = journeyTemplateName,
                RouteDistance = RouteDistance,
                Lifts = Lifts.Select(lift => new LiftTemplate { LiftDistance = lift.LiftDistance, PassengerName = lift.PassengerName }).ToList()
            };
        }

        private void RemoveSetting(string journeyTemplateName)
        {
            try
            {
                _commandDispatcher.Dispatch(new StoreJourneyTemplatesCommand(Enumerable.Empty<JourneyTemplate>(), new[] { journeyTemplateName }));
                var removedTemplate = JourneyTemplates.FirstOrDefault(t => t.Name == journeyTemplateName);
                JourneyTemplates.Remove(removedTemplate);
            }
            catch (Exception e)
            {
                Notification.Replace(new ErrorNotification(e.Message));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
