using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
