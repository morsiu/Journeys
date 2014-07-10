using Journeys.Application.Client.Wpf.Infrastructure.Extensions;
using Journeys.Data.Queries.Dtos.JourneysByPassengerThenMonthThenDay;
using System.ComponentModel;

namespace Journeys.Application.Client.Wpf.Features.ShowJourneysInCalendar
{
    internal sealed class JourneyDaySummary : INotifyPropertyChanged
    {
        private Value _value;

        public JourneyDaySummary(Value value)
        {
            _value = value;
        }

        public void Change(Value value)
        {
            _value = value;
            PropertyChanged.Raise(this, () => LiftSummary);
            PropertyChanged.Raise(this, () => JourneySummary);
        }

        public string LiftSummary { get { return string.Format("{0} / {1}", _value.LiftCount, _value.LiftDistance); } }

        public string JourneySummary { get { return string.Format("{0} / {1}", _value.JourneyCount, _value.JourneyDistance); } }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
