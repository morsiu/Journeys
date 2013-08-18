using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Journeys.Client.Wpf.Features.ShowJourneysByPassengerAndDay
{
    internal class JourneysByPassengerThenDayControl : Control
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var refreshButton = Template.FindName("RefreshButton", this) as ButtonBase;
            if (refreshButton != null)
            {
                BindingOperations.SetBinding(refreshButton, ButtonBase.CommandProperty, new Binding("RefreshCommand"));
            }
        }
    }
}
