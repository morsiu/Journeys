using System;

namespace Mors.Journeys.Application.QueryHandlers.Infrastructure
{
    internal sealed class Period
    {
        private readonly DateTime _end;
        private readonly DateTime _start;
        private readonly bool _isEmpty;

        public Period(DateTime start, DateTime end)
        {
            if (start <= end)
            {
                _start = start;
                _end = end;
            }
            else
            {
                _isEmpty = true;
            }
        }

        public bool Contains(DateTime point)
        {
            if (_isEmpty) return false;
            return _start <= point && _end >= point;
        }
    }
}
