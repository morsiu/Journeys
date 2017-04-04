using System.Collections.Generic;
using Mors.Journeys.Application.Client.Wpf.Commands;
using Mors.Journeys.Application.Client.Wpf.Queries;
using Mors.Journeys.Application.Client.Wpf.Settings;
using System.Windows;

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

        public UIElement Bootstrap()
        {
            _queryHandlerRegistry.SetHandler<GetJourneyTemplatesQuery, IEnumerable<JourneyTemplate>>(Settings.Settings.Default.Execute);
            _commandHandlerRegistry.SetHandler<StoreJourneyTemplatesCommand>(Settings.Settings.Default.Handle);
            return new MainPanel(
                _commandDispatcher,
                _queryDispatcher,
                _eventBus,
                _idFactory);
        }
    }
}
