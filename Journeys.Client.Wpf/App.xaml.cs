using System.Windows;

namespace Journeys.Client.Wpf
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            MainWindow = bootstrapper.Bootstrap();
            MainWindow.Show();
        }
    }
}
