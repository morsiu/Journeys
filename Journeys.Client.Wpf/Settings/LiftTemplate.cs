using System;

namespace Journeys.Client.Wpf.Settings
{
    [Serializable]
    public sealed class LiftTemplate
    {
        public string PassengerName { get; set; }

        public decimal LiftDistance { get; set; }

        public LiftTemplate Clone()
        {
            return new LiftTemplate
            {
                PassengerName = PassengerName,
                LiftDistance = LiftDistance
            };
        }
    }
}
