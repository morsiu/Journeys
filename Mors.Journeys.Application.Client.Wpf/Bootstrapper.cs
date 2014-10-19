using System.Collections.Generic;
using Mors.AppPlatform.Common.Services;
using Mors.Journeys.Application.Client.Wpf.Commands;
using Mors.Journeys.Application.Client.Wpf.Queries;
using Mors.Journeys.Application.Client.Wpf.Settings;

namespace Mors.Journeys.Application.Client.Wpf
{
    public sealed class Bootstrapper
    {
        private readonly IEventBus _eventBus;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IIdFactory _idFactory;
        private readonly IQueryHandlerRegistry _queryHandlerRegistry;
        private readonly ICommandHandlerRegistry _commandHandlerRegistry;

        public Bootstrapper(
            IEventBus eventBus,
            ICommandDispatcher commandDispatcher,
            ICommandHandlerRegistry commandHandlerRegistry,
            IQueryDispatcher queryDispatcher,
            IQueryHandlerRegistry queryHandlerRegistry,
            IIdFactory idFactory)
        {
            _eventBus = eventBus;
            _commandDispatcher = commandDispatcher;
            _commandHandlerRegistry = commandHandlerRegistry;
            _queryDispatcher = queryDispatcher;
            _queryHandlerRegistry = queryHandlerRegistry;
            _idFactory = idFactory;
        }

        public void Bootstrap()
        {
            _queryHandlerRegistry.SetHandler<GetJourneyTemplatesQuery, IEnumerable<JourneyTemplate>>(Properties.Settings.Default.Execute);
            _commandHandlerRegistry.SetHandler<StoreJourneyTemplatesCommand>(Properties.Settings.Default.Handle);
        }

        public void Run()
        {
            var application = new Application();
            application.InitializeComponent();
            var window = new MainWindow(
                _commandDispatcher,
                _queryDispatcher,
                _eventBus,
                _idFactory);
            application.Run(window);
        }
    }
}
