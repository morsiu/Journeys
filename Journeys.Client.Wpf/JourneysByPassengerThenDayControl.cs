using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Journeys.Client.Wpf
{
    internal class JourneysByPassengerThenDayControl : Control
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var refreshButton = Template.FindName("RefreshButton", this) as ButtonBase;
            if (refreshButton != null)
            {
                refreshButton.Click += OnRefreshButtonClick;
            }
        }

        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            var refreshCommand = (ICommand)(DataContext as dynamic).RefreshCommand;
            if (refreshCommand != null)
            {
                refreshCommand.Execute(null);
            }
        }
    }
}
