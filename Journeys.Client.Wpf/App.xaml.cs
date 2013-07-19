using System.Windows;
using Journeys.Application;

namespace Journeys.Client.Wpf
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Bootstrap();
            MainWindow = new MainWindow(bootstrapper.CommandDispatcher);
            MainWindow.Show();
        }
    }
}
