using Journeys.Application.Client.Wpf.Settings;
using Journeys.Data.Queries;
using System.Collections.Generic;

namespace Journeys.Application.Client.Wpf.Queries
{
    internal sealed class GetJourneyTemplatesQuery : IQuery<IEnumerable<JourneyTemplate>>
    {
    }
}
