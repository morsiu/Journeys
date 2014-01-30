using System.Collections.Generic;
using System.Linq;

namespace Journeys.Client.Wpf.Settings
{
    internal sealed class JourneyTemplate
    {
        public string Name { get; set; }

        public decimal Distance { get; set; }

        public List<LiftTemplate> Lifts { get; set; }

        public JourneyTemplate Clone()
        {
            return new JourneyTemplate
            {
                Name = Name,
                Distance = Distance,
                Lifts = Lifts.Select(t => t.Clone()).ToList()
            };
        }
    }
}
