using System.Windows.Input;

namespace Mors.Journeys.Application.Client.Wpf.Components.Settings
{
    internal static class SettingsCommands
    {
        public static readonly RoutedCommand LoadSettingCommand = new RoutedCommand("LoadSetting", typeof(SettingsCommands));

        public static readonly RoutedCommand RemoveSettingCommand = new RoutedCommand("RemoveSetting", typeof(SettingsCommands));

        public static readonly RoutedCommand SaveSettingCommand = new RoutedCommand("SaveSetting", typeof(SettingsCommands));
    }
}
