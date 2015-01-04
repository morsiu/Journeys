using System.Collections.Generic;
using Mors.Journeys.Application;
using Mors.Journeys.Application.Client.Wpf.Settings;
using Mors.Journeys.Common;

namespace Mors.Journeys.Application.Client.Wpf.Queries
{
    internal sealed class GetJourneyTemplatesQuery : IQuery<IEnumerable<JourneyTemplate>>
    {
    }
}
