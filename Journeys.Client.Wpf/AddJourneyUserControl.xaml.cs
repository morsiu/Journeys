using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Journeys.Client.Wpf
{
    public partial class AddJourneyUserControl : UserControl
    {
        public AddJourneyUserControl()
        {
            InitializeComponent();
        }

        private void OnAddJourneyClick(object sender, RoutedEventArgs e)
        {
            BindingGroup.CommitEdit();
            if (BindingGroup.HasValidationError)
                return;
            var addJourneyCommand = (ICommand)(DataContext as dynamic).AddJourneyCommand;
            addJourneyCommand.Execute(null);
        }
    }
}
