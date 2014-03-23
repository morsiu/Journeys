using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Capabilities
{
    [ValueObject]
    internal struct Distance
    {
        private readonly Point _from;
        private readonly Point _to;

        public Distance(Point from, Point to)
        {
            _from = from;
            _to = to;
        }

        public Point From { get { return _from; } }

        public Point To { get { return _to; } }

        public decimal Length { get { return _to.Distance - _from.Distance; } }
    }
}
