using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Application.Infrastructure.Commands
{
    internal delegate void CommandHandler<TCommand>(TCommand command);
}
