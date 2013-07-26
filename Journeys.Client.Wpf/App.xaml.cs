using System.Windows;
using Journeys.Application;
using Journeys.Eventing;

namespace Journeys.Client.Wpf
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var eventBus = new EventBus();
            var commandBootstrapper = new Journeys.Application.Bootstrapper(eventBus);
            var dataBootstrapper = new Journeys.Data.Bootstrapper(eventBus);
            dataBootstrapper.Bootstrap();
            commandBootstrapper.Bootstrap(dataBootstrapper.QueryDispatcher);
            var viewEventBus = new Infrastructure.EventBus();
            MainWindow = new MainWindow(commandBootstrapper.CommandDispatcher, dataBootstrapper.QueryDispatcher, viewEventBus);
            MainWindow.Show();
        }
    }
}
