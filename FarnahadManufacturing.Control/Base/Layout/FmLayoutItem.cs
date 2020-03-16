using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.LayoutControl;
using FarnahadManufacturing.Control.Base.Label;

namespace FarnahadManufacturing.Control.Base.Layout
{
    public class FmLayoutItem : LayoutItem
    {
        public FmLayoutItem()
        {
            this.AddColonToLabel = true;
            LabelWidth = 100;
            this.Loaded += OnLoaded;
            this.IsEnabledChanged += OnIsEnabledChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AddPropertyIsRequiredInLabel();
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            AddPropertyIsRequiredInLabel();
        }

        private bool _isRequiredPropFlag;
        private void AddPropertyIsRequiredInLabel()
        {
            if (this.IsActuallyRequired && this.IsEnabled && !_isRequiredPropFlag && this.LabelElement != null)
            {
                var redStyle = new Style
                { Setters = { new Setter(ForegroundProperty, Brushes.Red) } };
                var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                stackPanel.Children.Add(new LayoutItemLabel { Content = this.LabelElement.Content });
                stackPanel.Children.Add(new FmLabel { Text = "(*)", Style = redStyle });
                this.Label = stackPanel;
                _isRequiredPropFlag = true;
            }
        }

    }
}
