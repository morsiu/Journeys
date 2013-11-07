using Journeys.Client.Wpf.Infrastructure.Extensions;
using Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay;
using System.ComponentModel;

namespace Journeys.Client.Wpf.Features.ShowJourneysInCalendar
{
    internal class JourneyDaySummary : INotifyPropertyChanged
    {
        private Value _value;

        public JourneyDaySummary(Value value)
        {
            _value = value;
        }

        public void Change(Value value)
        {
            _value = value;
            PropertyChanged.Raise(() => LiftDistance);
            PropertyChanged.Raise(() => LiftCount);
            PropertyChanged.Raise(() => JourneyDistance);
            PropertyChanged.Raise(() => JourneyCount);
        }

        public decimal LiftDistance { get { return _value.LiftDistance; } }

        public int LiftCount { get { return _value.LiftCount; } }

        public decimal JourneyDistance { get { return _value.JourneyDistance; } }

        public int JourneyCount { get { return _value.JourneyCount; } }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
