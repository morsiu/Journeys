using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
