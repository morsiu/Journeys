using Journeys.Client.Wpf.Commands;
using Journeys.Client.Wpf.Queries;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Client.Wpf.Settings
{
    internal sealed partial class Settings
    {
        public List<JourneyTemplate> Execute(GetJourneyTemplatesQuery query)
        {
            return JourneyTemplates == null
                ? new List<JourneyTemplate>()
                : JourneyTemplates.Templates.Select(t => t.Clone()).ToList();
        }

        internal void Handle(StoreJourneyTemplatesCommand command)
        {
            var templates = JourneyTemplates.Templates;
            foreach (var newTemplate in command.AddedTemplates)
            {
                var existingTemplate = templates.FirstOrDefault(t => t.Name == newTemplate.Name);
                if (existingTemplate == null)
                {
                    templates.Remove(existingTemplate);
                }
                templates.Add(newTemplate.Clone());
            }
            foreach (var removedTemplateName in command.RemovedTemplateNames)
            {
                var removedTemplate = templates.FirstOrDefault(t => t.Name == removedTemplateName);
                if (removedTemplate != null)
                {
                    templates.Remove(removedTemplate);
                }
            }
            Save();
        }
    }
}
