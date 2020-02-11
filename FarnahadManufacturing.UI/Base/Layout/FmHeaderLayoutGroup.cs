using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.LayoutControl;
using FarnahadManufacturing.UI.Base.Label;
using Brushes = System.Windows.Media.Brushes;

namespace FarnahadManufacturing.UI.Base.Layout
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
