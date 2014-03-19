using Journeys.Common;
using Journeys.Domain.Infrastructure.Markers;
using System.Collections.Generic;

namespace Journeys.Domain.Expenses.Operations
{
    [Aggregate]
    public class Journey
    {
        private readonly Distance _distance;
        private readonly IId _id;
        private readonly IReadOnlyCollection<Lift> _lifts;

        public Journey(IId id, Distance distance, IReadOnlyCollection<Lift> lifts)
        {
            _distance = distance;
            _id = id;
            _lifts = lifts;
        }

        public Distance Distance { get { return _distance; } }

        public IId Id { get { return _id; } }

        public IReadOnlyCollection<Lift> Lifts { get { return _lifts; } }
    }
}
