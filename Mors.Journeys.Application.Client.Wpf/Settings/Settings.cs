using System;
using System.Collections.Generic;
using System.Linq;
using Mors.Journeys.Application.Client.Wpf.Commands;
using Mors.Journeys.Application.Client.Wpf.Queries;

namespace Mors.Journeys.Application.Client.Wpf.Settings
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
            if (JourneyTemplates == null) JourneyTemplates = new JourneyTemplateCollection { Templates = new List<JourneyTemplate>() };
            var templates = JourneyTemplates.Templates;
            foreach (var newTemplate in command.AddedTemplates)
            {
                if (string.IsNullOrWhiteSpace(newTemplate.Name)) throw new InvalidOperationException("Journey template name cannot be empty.");
                var existingTemplate = templates.FirstOrDefault(t => t.Name == newTemplate.Name);
                if (existingTemplate != null)
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
