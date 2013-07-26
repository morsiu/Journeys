
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Journeys.Client.Wpf
{
    internal class AddJourneyControl : Control
    {
        public AddJourneyControl()
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
