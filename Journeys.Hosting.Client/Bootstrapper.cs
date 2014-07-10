using System;
using Journeys.Hosting.Adapters.Modules.WpfClient;
using Journeys.Support.Dispatching;
using Journeys.Support.Repositories;

namespace Journeys.Hosting.Client
{
    internal class Bootstrapper
    {
        public void Bootstrap()
        {
            var eventBus = new Support.Events.EventBus();
            var idFactory = new GuidIdFactory();
            var handlerRegistry = new HandlerRegistry();
            var handlerDispatcher = new HandlerDispatcher(handlerRegistry);

            var wpfClientBootstrapper = new Application.Client.Wpf.Bootstrapper(
                new WpfClientEventBus(eventBus),
                new WpfClientCommandDispatcher(new Uri("http://localhost:65363/api/command"), handlerDispatcher),
                new WpfClientCommandHandlerRegistry(handlerRegistry),
                new WpfClientQueryDispatcher(new Uri("http://localhost:65363/api/query"), handlerDispatcher),
                new WpfClientQueryHandlerRegistry(handlerRegistry),
                new WpfClientIdFactory(idFactory));
            wpfClientBootstrapper.Bootstrap();
            wpfClientBootstrapper.Run();
        }
    }
}
