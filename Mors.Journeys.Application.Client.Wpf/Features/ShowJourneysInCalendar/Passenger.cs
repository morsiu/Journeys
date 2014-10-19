using Mors.Journeys.Data.Queries.Dtos;

namespace Mors.Journeys.Application.Client.Wpf.Features.ShowJourneysInCalendar
{
    internal sealed class Passenger
    {
        private readonly PersonName _passengerName;

        public Passenger(PersonName passengerName)
        {
            _passengerName = passengerName;
        }

        public string Name { get { return _passengerName.Name; } }

        public object Id { get { return _passengerName.OwnerId; } }
    }
}
