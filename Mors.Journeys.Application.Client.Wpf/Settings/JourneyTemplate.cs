using System;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Application.Client.Wpf.Settings
{
    [Serializable]
    public sealed class JourneyTemplate
    {
        public string Name { get; set; }

        public decimal RouteDistance { get; set; }

        public List<LiftTemplate> Lifts { get; set; }

        public JourneyTemplate Clone()
        {
            return new JourneyTemplate
            {
                Name = Name,
                RouteDistance = RouteDistance,
                Lifts = Lifts.Select(t => t.Clone()).ToList()
            };
        }
    }
}
