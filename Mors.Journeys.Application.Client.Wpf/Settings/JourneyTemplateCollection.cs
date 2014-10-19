﻿using System;
using System.Collections.Generic;

namespace Mors.Journeys.Application.Client.Wpf.Settings
{
    [Serializable]
    public sealed class JourneyTemplateCollection
    {
        public List<JourneyTemplate> Templates { get; set; }
    }
}
