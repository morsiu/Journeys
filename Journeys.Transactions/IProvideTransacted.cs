using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Transactions
{
    public interface IProvideTransacted<T>
    {
        ITransacted<T> Lift();
    }
}
