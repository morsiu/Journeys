using Journeys.Command;
using Journeys.Common;
using Journeys.Repositories;

namespace Journeys.Client.Wpf.Adapters
{
    internal class CommandIdFactory : IIdFactory
    {
        private readonly IdFactory _idFactory;

        public CommandIdFactory(IdFactory idFactory)
        {
            _idFactory = idFactory;
        }

        public IId Create()
        {
            return _idFactory.Create();
        }
    }
}
