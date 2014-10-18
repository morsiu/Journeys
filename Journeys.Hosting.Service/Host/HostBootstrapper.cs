using System.Collections.Generic;
using System.IO;
using Journeys.Hosting.Adapters.Modules.Service;
using Journeys.Hosting.Service.Modules;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace Journeys.Hosting.Service.Host
{
    internal sealed class HostBootstrapper : DefaultNancyBootstrapper
    {
        private readonly ServiceQueryDispatcher _queryDispatcher;
        private readonly ServiceCommandDispatcher _commandDispatcher;
        private readonly string _sitePath;

        public HostBootstrapper(ServiceQueryDispatcher queryDispatcher, ServiceCommandDispatcher commandDispatcher, string sitePath)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _sitePath = Path.GetFullPath(sitePath);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.AddDirectory("/site", "site");
            nancyConventions.StaticContentsConventions.AddFile("/site/", "site/index.html");
            base.ConfigureConventions(nancyConventions);
        }

        protected override IRootPathProvider RootPathProvider
        {
            get
            {
                return new StaticRootPathProvider(_sitePath);
            }
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
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

        private class StaticRootPathProvider : IRootPathProvider
        {
            private readonly string _rootPath;

            public StaticRootPathProvider(string rootPath)
            {
                _rootPath = rootPath;
            }

            public string GetRootPath()
            {
                return _rootPath;
            }
        }
    }
}
