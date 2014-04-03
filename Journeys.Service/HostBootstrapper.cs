using Journeys.Adapters;
using Journeys.Service.Modules;
using Nancy;
using Nancy.Bootstrapper;
using System.Collections.Generic;

namespace Journeys.Service
{
    internal sealed class HostBootstrapper : DefaultNancyBootstrapper
    {
        private readonly ServiceQueryDispatcher _queryDispatcher;
        private readonly ServiceCommandDispatcher _commandDispatcher;

        public HostBootstrapper(ServiceQueryDispatcher queryDispatcher, ServiceCommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        protected override void ConfigureRequestContainer(Nancy.TinyIoc.TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Register(typeof(ServiceQueryDispatcher), _queryDispatcher);
            container.Register(typeof(ServiceCommandDispatcher), _commandDispatcher);
        }

        protected override IEnumerable<ModuleRegistration> Modules
        {
            get
            {
                yield return new ModuleRegistration(typeof(QueryModule));
                yield return new ModuleRegistration(typeof(CommandModule));
            }
        }
    }
}
