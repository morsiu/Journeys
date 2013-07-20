using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Application.Infrastructure.Transactions
{
    internal interface ITransacted<T>
    {
        T Object { get; }

        void Abort();

        void Commit();
    }
}
