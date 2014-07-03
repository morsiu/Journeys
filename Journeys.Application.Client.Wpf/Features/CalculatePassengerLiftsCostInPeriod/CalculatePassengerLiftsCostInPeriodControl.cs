using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Journeys.Application.Client.Wpf.Features.CalculatePassengerLiftsCostInPeriod
{
    internal class CalculatePassengerLiftsCostInPeriodControl : Control
    {
        public CalculatePassengerLiftsCostInPeriodControl()
        {
            BindingGroup = new BindingGroup();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var calculateButton = GetTemplateChild("CalculateButton") as ButtonBase;
            if (calculateButton != null)
            {
                calculateButton.Click += OnCalculateButtonClick;
            }
        }

        private void OnCalculateButtonClick(object sender, RoutedEventArgs e)
        {
            BindingGroup.CommitEdit();
            if (BindingGroup.HasValidationError)
                return;
            var calculateCommand = (ICommand)(DataContext as dynamic).CalculateCommand;
            calculateCommand.Execute(null);
        }
    }
}
