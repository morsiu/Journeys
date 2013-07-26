using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Commands
{
    public class AddJourneyCommand
    {
        public Guid JourneyId { get; private set; }

        public decimal JourneyDistance { get; private set; }

        public DateTime JourneyDateOfOccurence { get; private set; }

        public string PersonName { get; private set; }

        public decimal LiftDistance { get; private set; }

        public AddJourneyCommand(Guid journeyId, decimal journeyDistance, DateTime journeyDateOfOccurence, string personName, decimal liftDistance)
        {
            JourneyId = journeyId;
            JourneyDistance = journeyDistance;
            JourneyDateOfOccurence = journeyDateOfOccurence;
            PersonName = personName;
            LiftDistance = liftDistance;
        }
    }
}
