using System.Collections.Generic;
using System.Linq;
using Mors.Journeys.Application.Client.Wpf.Settings;

namespace Mors.Journeys.Application.Client.Wpf.Commands
{
    internal sealed class StoreJourneyTemplatesCommand
    {
        public StoreJourneyTemplatesCommand(
            IEnumerable<JourneyTemplate> addedTemplates,
            IEnumerable<string> removedTemplateNames)
        {
            AddedTemplates = addedTemplates.ToList();
            RemovedTemplateNames = removedTemplateNames.ToList();
        }

        public List<JourneyTemplate> AddedTemplates { get; private set; }

        public List<string> RemovedTemplateNames { get; private set; }
    }
}
