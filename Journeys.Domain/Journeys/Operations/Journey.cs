using Journeys.Domain.Identities;
using Journeys.Domain.Markers;
using Journeys.Domain.Routes.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Journeys.Operations
{
    [Entity]
    public class Journey
    {
        private DateTime _dateOfOccurence;
        private Id<Route> _routeId;
        private List<Lift> _lifts = new List<Lift>();

        public Journey(DateTime dateOfOccurence, Id<Route> routeId)
        {
            _dateOfOccurence = dateOfOccurence;
            _routeId = routeId;
        }

        public void AddLift(Lift lift)
        {
            _lifts.Add(lift);
        }
    }
}
