using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Queries.Dtos
{
    public class JourneyWithLift
    {
        public Guid JourneyId { get; set; }

        public Guid? PassengerId { get; set; }

        public DateTime DateOfOccurrence { get; set; }

        public decimal Distance { get; set; }

        public string PassengerName { get; set; }

        public decimal? PassengerLiftDistance { get; set; }
    }
}
