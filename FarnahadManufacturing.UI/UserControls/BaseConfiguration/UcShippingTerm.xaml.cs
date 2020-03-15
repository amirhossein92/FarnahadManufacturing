using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcShippingTerm : FilterUserControlPage
    {
        private ObservableCollection<ShippingTerm> _shippingTerms;
        private ShippingTerm _activeShippingTerm;

        public UcShippingTerm()
        {
            InitializeComponent();

            UserControlTitle = "شرایط تحویل کالا";
            ImagePath = "Icons/NavigationBar/ShippingTerm_Small.svg";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchGridControl();
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text);
        }

        private void LoadSearchGridControlData(string title)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var shippingTermsQueryable = dbContext.ShippingTerms.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    shippingTermsQueryable = shippingTermsQueryable.Where(item => item.Title.Contains(title));
                var totalRecordsCount = shippingTermsQueryable.Count();
                _shippingTerms = shippingTermsQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _shippingTerms;
                PaginationUserControl.UpdateRecordsDetail(_shippingTerms.Count, totalRecordsCount);
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activeShippingTerm = new ShippingTerm();
            FillData(_activeShippingTerm);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeShippingTerm);
            if (_activeShippingTerm.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeShippingTerm).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    IsEditing();
                }
            }
            else
            {
                _activeShippingTerm.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeShippingTerm.CreatedDateTime = ApplicationSessionService.GetNowDateTime();

                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.ShippingTerms.Add(_activeShippingTerm);
                    dbContext.SaveChanges();
                }

                OnAddToolBarItem();
            }

            MessageBoxService.SaveConfirmation(_activeShippingTerm.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeShippingTerm.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var shippingTerm = dbContext.ShippingTerms.Find(_activeShippingTerm.Id);
                    dbContext.ShippingTerms.Remove(shippingTerm);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeShippingTerm = new ShippingTerm();
                IsNotEditingAndAdding();
            }
        }

        protected override void OnAdding()
        {
            NameTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            NameTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeShippingTerm.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInactiveHeaderTitle(UserControlTitle);
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            PaginationUserControl.CurrentPage = 1;
            LoadSearchGridControl();
        }

        private void SearchGridControlOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var searchGridControl = (GridControl)sender;
            var rowIndex = searchGridControl.View.GetRowHandleByMouseEventArgs(e);
            if (rowIndex == DataControlBase.InvalidRowHandle)
                return;
            if (searchGridControl.ItemsSource is ObservableCollection<ShippingTerm> shippingTerms)
                EditData(shippingTerms[rowIndex]);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var shippingTerm = SearchGridControl.SelectedItem as ShippingTerm;
            EditData(shippingTerm);
        }

        private void EditData(ShippingTerm shippingTerm)
        {
            _activeShippingTerm = shippingTerm;
            FillData(_activeShippingTerm);
            IsEditing();
        }

        private void FillData(ShippingTerm shippingTerm)
        {
            NameTextEdit.Text = shippingTerm.Title;
            DescriptionTextEdit.Text = shippingTerm.Description;
        }

        private void ReadData(ShippingTerm shippingTerm)
        {
            shippingTerm.Title = NameTextEdit.Text;
            shippingTerm.Description = DescriptionTextEdit.Text;
        }
    }
}
