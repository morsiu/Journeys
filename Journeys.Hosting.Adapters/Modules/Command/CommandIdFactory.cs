using Journeys.Application.Command;
using Journeys.Support.Repositories;

namespace Journeys.Hosting.Adapters.Modules.Command
{
    public sealed class CommandIdFactory : IIdFactory
    {
        private readonly GuidIdFactory _idFactory;

        public CommandIdFactory(GuidIdFactory idFactory)
        {
            _idFactory = idFactory;
        }

        public object Create()
        {
            return _idFactory.Create();
        }
    }
}
