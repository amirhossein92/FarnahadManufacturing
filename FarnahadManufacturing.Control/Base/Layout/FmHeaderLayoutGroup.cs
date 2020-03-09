using System;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.LayoutControl;
using FarnahadManufacturing.Control.Base.Label;

// CHECK
namespace FarnahadManufacturing.Control.Base.Layout
{
    public class FmHeaderLayoutGroup : LayoutGroup
    {
        public FmHeaderLayoutGroup()
        {
            Height = 30;
            Background = Brushes.LightGray;
            var headerTextBlock = new FmHeaderLabel();
            Children.Add(headerTextBlock);
        }

        public static readonly DependencyProperty HeaderTitleProperty = DependencyProperty.Register(
            "HeaderTitle", typeof(string), typeof(FmHeaderLayoutGroup), new PropertyMetadata(default(string), HeaderTitleChangedCallback));

        private static void HeaderTitleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var headerLayoutGroup = (FmHeaderLayoutGroup)d;
            if (headerLayoutGroup.Children[0] is FmHeaderLabel headerTextBlock)
                headerTextBlock.Text = Convert.ToString(e.NewValue);
        }

        public string HeaderTitle
        {
            get => (string)GetValue(HeaderTitleProperty);
            set => SetValue(HeaderTitleProperty, value);
        }
    }
}
