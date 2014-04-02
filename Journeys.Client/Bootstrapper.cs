using Journeys.Adapters;
using Journeys.Dispatching;
using Journeys.Repositories;
using System;

namespace Journeys.Client
{
    internal class Bootstrapper
    {
        public void Bootstrap()
        {
            var eventBus = new Event.EventBus();
            var idFactory = new IdFactory();
            var handlerRegistry = new HandlerRegistry();
            var handlerDispatcher = new HandlerDispatcher(handlerRegistry);

            var wpfClientBootstrapper = new Client.Wpf.Bootstrapper(
                new WpfClientEventBus(eventBus),
                new WpfClientCommandDispatcher(new Uri("http://localhost:65363/api/command/"), handlerDispatcher),
                new WpfClientCommandHandlerRegistry(handlerRegistry),
                new WpfClientQueryDispatcher(new Uri("http://localhost:65363/api/query/"), handlerDispatcher),
                new WpfClientQueryHandlerRegistry(handlerRegistry),
                new WpfClientIdFactory(idFactory));
            wpfClientBootstrapper.Bootstrap();
            wpfClientBootstrapper.Run();
        }
    }
}
