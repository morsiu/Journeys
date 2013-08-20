using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Journeys.Client.Wpf.Features.AddJourneysWithLifts
{
    internal class AddJourneyWithLiftControl : Control
    {
        public AddJourneyWithLiftControl()
        {
            BindingGroup = new BindingGroup();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var addButton = Template.FindName("AddButton", this) as ButtonBase;
            if (addButton != null)
            {
                addButton.Click += OnAddButtonClick;
            }
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            BindingGroup.CommitEdit();
            if (BindingGroup.HasValidationError)
                return;
            var addJourneyCommand = (ICommand)(DataContext as dynamic).AddJourneyCommand;
            addJourneyCommand.Execute(null);
        }
    }
}
