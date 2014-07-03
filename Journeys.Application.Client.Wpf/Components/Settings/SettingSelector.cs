using Journeys.Application.Client.Wpf.Components.Settings;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Journeys.Application.Client.Wpf.Components.Settings
{
    [TemplatePart(Name = "PART_Selector", Type = typeof(Selector))]
    [TemplatePart(Name = "PART_Load", Type = typeof(ButtonBase))]
    [TemplatePart(Name = "PART_Save", Type = typeof(ButtonBase))]
    [TemplatePart(Name = "PART_Remove", Type = typeof(ButtonBase))]
    internal sealed class SettingSelector : Control
    {
        public static readonly DependencyProperty SettingNamePathProperty = DependencyProperty.Register("SettingNamePath", typeof(string), typeof(SettingSelector));

        public static readonly DependencyProperty SettingsSourceProperty = DependencyProperty.Register("SettingsSource", typeof(IEnumerable), typeof(SettingSelector));

        public string SettingNamePath
        {
            get { return (string)GetValue(SettingNamePathProperty); }
            set { SetValue(SettingNamePathProperty, value); }
        }

        public IEnumerable SettingsSource
        {
            get { return (IEnumerable)GetValue(SettingsSourceProperty); }
            set { SetValue(SettingsSourceProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var selector = GetTemplateChild("PART_Selector");
            if (selector != null)
            {
                BindingOperations.SetBinding(selector, ItemsControl.ItemsSourceProperty, new Binding("SettingsSource") { Source = this });
                BindingOperations.SetBinding(selector, Selector.DisplayMemberPathProperty, new Binding("SettingNamePath") { Source = this });
                var loadButton = GetTemplateChild("PART_Load") as ButtonBase;
                if (loadButton != null)
                {
                    BindingOperations.SetBinding(loadButton, ButtonBase.CommandParameterProperty, new Binding("Text") { Source = selector });
                    loadButton.Command = SettingsCommands.LoadSettingCommand;
                }
                var saveButton = GetTemplateChild("PART_Save") as ButtonBase;
                if (saveButton != null)
                {
                    BindingOperations.SetBinding(saveButton, ButtonBase.CommandParameterProperty, new Binding("Text") { Source = selector });
                    saveButton.Command = SettingsCommands.SaveSettingCommand;
                }
                var removeButton = GetTemplateChild("PART_Remove") as ButtonBase;
                if (removeButton != null)
                {
                    BindingOperations.SetBinding(removeButton, ButtonBase.CommandParameterProperty, new Binding("Text") { Source = selector });
                    removeButton.Command = SettingsCommands.RemoveSettingCommand;
                }
            }
        }
    }
}
