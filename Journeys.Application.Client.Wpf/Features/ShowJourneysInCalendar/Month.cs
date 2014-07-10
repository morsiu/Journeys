using System.Globalization;
namespace Journeys.Application.Client.Wpf.Features.ShowJourneysInCalendar
{
    internal sealed class Month
    {
        public Month(int year, int monthInYear) 
        {
            Year = year;
            MonthInYear = monthInYear;
        }

        public int Year { get; private set; }

        public int MonthInYear { get; private set; }

        public string MonthName
        {
            get { return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(MonthInYear); }
        }

        public Month Next()
        {
            return MonthInYear >= 12
                ? new Month(Year + 1, 1)
                : new Month(Year, MonthInYear + 1);
        }

        public Month Previous() 
        {
            return MonthInYear <= 1
                ? new Month(Year - 1, 12)
                : new Month(Year, MonthInYear - 1);
        }

        public override bool Equals(object obj)
        {
            return obj is Month
                && Equals((Month)obj);
        }

        public bool Equals(Month other)
        {
            return other != null
                && other.MonthInYear == MonthInYear
                && other.Year == Year;
        }

        public override int GetHashCode()
        {
            return (MonthInYear.GetHashCode() * 37) ^ Year.GetHashCode();
        }
    }
}
