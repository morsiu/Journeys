using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Client.Wpf
{
    internal class SuccessNotification
    {
        public SuccessNotification(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
