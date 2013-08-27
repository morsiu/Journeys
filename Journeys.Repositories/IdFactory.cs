using System;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Common;

namespace Journeys.Repositories
{
    public class IdFactory
    {
        public IId Create()
        {
            return new Id(Guid.NewGuid());
        }

        public Type IdImplementationType
        {
            get { return typeof(Id); }
        }
    }
}
