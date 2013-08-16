using Journeys.Application;
using Journeys.Application.Commands;
using Journeys.Client.Wpf.Events;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Commands;
using Journeys.Data;
using Journeys.Data.Journeys;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Journeys.Client.Wpf
{
    public partial class MainWindow : Window
    {
        internal MainWindow(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, EventBus eventBus)
        {
            InitializeComponent();
            AddJourney.DataContext = new AddJourneyViewModel(commandDispatcher, eventBus);
            var journeysViewModel = new JourneysViewModel(eventBus, queryDispatcher);
            Journeys.DataContext = journeysViewModel;
            journeysViewModel.Reload();
        }        
    }
}
