using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Application.Commands
{
    public delegate void CommandHandler<TCommand>(TCommand command);
}
