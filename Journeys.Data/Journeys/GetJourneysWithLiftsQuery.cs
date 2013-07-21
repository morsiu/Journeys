using Journeys.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Data.Journeys
{
    public class GetJourneysWithLiftsQuery : IQuery<IEnumerable<JourneyWithLift>>
    {
        internal IEnumerable<JourneyWithLift> Execute(JourneyView view)
        {
            return view.GetAll()
                .Where(e => e.PassengerId.HasValue)
                .OrderBy(e => e.DateOfOccurence)
                .ThenBy(e => e.Id)
                .ThenBy(e => e.PassengerName)
                .ThenBy(e => e.PassengerId)
                .ToList();
        }
    }
}
