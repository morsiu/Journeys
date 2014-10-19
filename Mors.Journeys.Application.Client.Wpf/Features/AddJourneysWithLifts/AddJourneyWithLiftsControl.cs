using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using Mors.Journeys.Application.Client.Wpf.Components.Settings;

namespace Mors.Journeys.Application.Client.Wpf.Features.AddJourneysWithLifts
{
    internal sealed class AddJourneyWithLiftsControl : Control
    {
        public AddJourneyWithLiftsControl()
        {
            BindingGroup = new BindingGroup();
            CommandBindings.Add(new CommandBinding(SettingsCommands.LoadSettingCommand, OnLoadSetting));
            CommandBindings.Add(new CommandBinding(SettingsCommands.SaveSettingCommand, OnSaveSetting));
            CommandBindings.Add(new CommandBinding(SettingsCommands.RemoveSettingCommand, OnRemoveSetting));
        }

        private void OnRemoveSetting(object sender, ExecutedRoutedEventArgs e)
        {
            var removeSettingCommand = (ICommand)(DataContext as dynamic).RemoveSettingCommand;
            removeSettingCommand.Execute(e.Parameter);
        }

        private void OnSaveSetting(object sender, ExecutedRoutedEventArgs e)
        {
            BindingGroup.CommitEdit();
            var saveSettingCommand = (ICommand)(DataContext as dynamic).SaveSettingCommand;
            saveSettingCommand.Execute(e.Parameter);
        }

        private void OnLoadSetting(object sender, ExecutedRoutedEventArgs e)
        {
            var loadSettingCommand = (ICommand)(DataContext as dynamic).LoadSettingCommand;
            loadSettingCommand.Execute(e.Parameter);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var addButton = GetTemplateChild("AddButton") as ButtonBase;
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
