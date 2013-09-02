namespace Journeys.Client.Wpf
{
    public class Bootstrapper
    {
        private readonly IEventBus _eventBus;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IIdFactory _idFactory;

        public Bootstrapper(
            IEventBus eventBus,
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            IIdFactory idFactory)
        {
            _eventBus = eventBus;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _idFactory = idFactory;
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
