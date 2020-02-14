using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FarnahadManufacturing.UI.Base.Layout;

namespace FarnahadManufacturing.UI.Base.UserControl
{
    public partial class PaginationUserControl : FmHorizontalLayoutGroup
    {
        public PaginationUserControl()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler ClickOnPrevious;
        public event RoutedEventHandler ClickOnNext;

        private void PreviousPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            ClickOnPrevious?.Invoke(sender, e);
        }

        private void NextPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            ClickOnNext?.Invoke(sender, e);
        }

        public static readonly DependencyProperty RecordCountTextProperty = DependencyProperty.Register(
            "RecordCountText", typeof(string), typeof(PaginationUserControl), new PropertyMetadata(default(string), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PaginationUserControl)d;
            if (e.NewValue != null)
                control.RecordsCountFmLabel.Text = Convert.ToString(e.NewValue);
        }

        public string RecordCountText
        {
            get => (string)GetValue(RecordCountTextProperty);
            set => SetValue(RecordCountTextProperty, value);
        }
    }
}
