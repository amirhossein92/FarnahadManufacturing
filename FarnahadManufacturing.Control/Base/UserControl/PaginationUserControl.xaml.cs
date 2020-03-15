using System.Windows;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.Layout;

// CHECK
namespace FarnahadManufacturing.Control.Base.UserControl
{
    public partial class PaginationUserControl : FmHorizontalLayoutGroup
    {
        private int _currentPage = 1;
        private int _currentPageCount;
        private int _totalRecordsCount;

        public PaginationUserControl()
        {
            InitializeComponent();
        }

        public int CurrentPage
        {
            get => _currentPage;
            set => _currentPage = value;
        }

        public event RoutedEventHandler RefreshData;

        private void PreviousPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                RefreshData?.Invoke(sender, e);
            }
        }

        private void NextPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_currentPage < PaginationUtility.MaximumPageNumber(_totalRecordsCount))
            {
                _currentPage++;
                RefreshData?.Invoke(sender, e);
            }
        }

        public void UpdateRecordsDetail(int currentPageCount, int totalRecordsCount)
        {
            _currentPageCount = currentPageCount;
            _totalRecordsCount = totalRecordsCount;
            RecordsCountFmLabel.Text =
                PaginationUtility.GetRecordsDetailText(_currentPage, _currentPageCount, _totalRecordsCount);
        }
    }
}
