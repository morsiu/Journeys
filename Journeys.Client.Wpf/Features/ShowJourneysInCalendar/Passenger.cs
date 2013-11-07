using Journeys.Common;
using Journeys.Queries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Client.Wpf.Features.ShowJourneysInCalendar
{
    internal class Passenger
    {
        private readonly PersonName _passengerName;

        public Passenger(PersonName passengerName)
        {
            _passengerName = passengerName;
        }

        public string Name { get { return _passengerName.Name; } }

        public IId Id { get { return _passengerName.OwnerId; } }
    }
}
