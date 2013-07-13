using Journeys.Domain.Markers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Journeys.Operations
{
    [Aggregate]
    public class JourneyLog
    {
        private List<Journey> _journeys = new List<Journey>();

        public void AddJourney(Journey journey)
        {
            _journeys.Add(journey);
        }

        public int JourneyCount { get { return _journeys.Count; } }
    }
}
