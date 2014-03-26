using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Domain.Expenses.Operations
{
    public sealed class JourneyFactory
    {
        public JourneyBuilder Create(IId journeyId, decimal distance)
        {
            return new JourneyBuilder(journeyId, distance);
        }
    }
}
