using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Journeys.Application.Client.Wpf.Components.Popups
{
    [TemplatePart(Name = "PART_Toggle", Type = typeof(ToggleButton))]
    internal sealed class TogglePopup : ContentControl
    {
        public static readonly DependencyProperty ToggleContentProperty = DependencyProperty.Register("ToggleContent", typeof(object), typeof(TogglePopup));
        public static readonly DependencyProperty ToggleContentStringFormatProperty = DependencyProperty.Register("ToggleContentStringFormat", typeof(string), typeof(TogglePopup));
        public static readonly DependencyProperty ToggleContentTemplateProperty = DependencyProperty.Register("ToggleContentTemplate", typeof(DataTemplate), typeof(TogglePopup));
        public static readonly DependencyProperty ToggleContentTemplateSelectorProperty = DependencyProperty.Register("ToggleContentTemplateSelector", typeof(DataTemplateSelector), typeof(TogglePopup));

        public object ToggleContent
        {
            get { return GetValue(ToggleContentProperty); }
            set { SetValue(ToggleContentProperty, value); }
        }

        public string ToggleContentStringFormat
        {
            get { return (string)GetValue(ToggleContentStringFormatProperty); }
            set { SetValue(ToggleContentStringFormatProperty, value); }
        }

        public DataTemplate ToggleContentTemplate
        {
            get { return (DataTemplate)GetValue(ToggleContentTemplateProperty); }
            set { SetValue(ToggleContentTemplateProperty, value); }
        }

        public DataTemplateSelector ToggleContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ToggleContentTemplateSelectorProperty); }
            set { SetValue(ToggleContentTemplateSelectorProperty, value); }
        }

    }
}
