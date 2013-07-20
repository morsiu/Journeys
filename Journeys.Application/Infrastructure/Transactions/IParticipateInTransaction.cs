using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Application.Infrastructure.Transactions
{
    internal interface IParticipateInTransaction
    {
        void Abort();

        void Commit();
    }
}
