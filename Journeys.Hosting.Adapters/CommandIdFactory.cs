using Journeys.Application.Command;
using Mors.Support.Repositories;

namespace Journeys.Application.Adapters
{
    public class CommandIdFactory : IIdFactory
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
