using System;
using System.Windows;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.Layout;

namespace FarnahadManufacturing.Control.Base.UserControl
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

        //public static readonly DependencyProperty RecordCountTextProperty = DependencyProperty.Register(
        //    "RecordCountText", typeof(string), typeof(PaginationUserControl), new PropertyMetadata(default(string), PropertyChangedCallback));

        //private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var control = (PaginationUserControl)d;
        //    if (e.NewValue != null)
        //        control.RecordsCountFmLabel.Text = Convert.ToString(e.NewValue);
        //}

        //public string RecordCountText
        //{
        //    get => (string)GetValue(RecordCountTextProperty);
        //    set => SetValue(RecordCountTextProperty, value);
        //}

        public void UpdateRecordsDetail(int currentPage, int currentPageCount, int totalRecordsCount)
        {
            this.RecordsCountFmLabel.Text =
                PaginationUtility.GetRecordsDetailText(currentPage, currentPageCount, totalRecordsCount);
        }
    }
}
