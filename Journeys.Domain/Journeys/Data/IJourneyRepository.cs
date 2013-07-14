using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Markers;
using Journeys.Domain.Journeys.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Journeys.Data
{
    [Repository]
    public interface IJourneyRepository
    {
        Journey GetJourney(Id<Journey> id);

        void SaveJourney(Journey journey);
    }
}
