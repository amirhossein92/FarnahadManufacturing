using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core.ReflectionExtensions;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Common;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcTaxRate : FilterUserControlBase
    {
        private ObservableCollection<TaxRate> _taxRates;
        private TaxRate _activeTaxRate;

        public UcTaxRate()
        {
            InitializeComponent();

            UserControlTitle = "نرخ مالیاتی";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchTaxTypeComboBox();
            LoadSearchStatusComboBox();
            LoadSearchGridControl();
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchTaxTypeComboBoxEdit.EditValue, SearchStatusComboBoxEdit.EditValue);
        }

        private void LoadSearchGridControlData(string title, object taxTypeComboBox, object statusComboBox)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var taxRatesQueryable = dbContext.TaxRates.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    taxRatesQueryable = taxRatesQueryable.Where(item => item.Title.Contains(title));
                if (taxTypeComboBox is bool isPercentage)
                    taxRatesQueryable = taxRatesQueryable.Where(item => item.IsPercentageSelected == isPercentage);
                if (statusComboBox is bool isActive)
                    taxRatesQueryable = taxRatesQueryable.Where(item => item.IsActive == isActive);
                var totalRecordsCount = taxRatesQueryable.Count();
                _taxRates = taxRatesQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _taxRates;
                PaginationUserControl.UpdateRecordsDetail(_taxRates.Count, totalRecordsCount);
            }
        }

        private void LoadSearchTaxTypeComboBox()
        {
            var taxTypes = new ObservableCollection<FmComboModel<bool>>
            {
                new FmComboModel<bool>(true, "درصد"),
                new FmComboModel<bool>(false, "نرخ ثابت"),
            };
            SearchTaxTypeComboBoxEdit.ItemsSource = taxTypes;
        }

        private void LoadSearchStatusComboBox()
        {
            var statuses = new ObservableCollection<FmComboModel<bool>>
            {
                new FmComboModel<bool>(false, "غیر فعال"),
                new FmComboModel<bool>(true, "فعال"),
            };
            SearchStatusComboBoxEdit.ItemsSource = statuses;
        }


        protected override void OnAddToolBarItem()
        {
            _activeTaxRate = new TaxRate();
            FillData(_activeTaxRate);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeTaxRate);
            if (_activeTaxRate.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeTaxRate).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            else
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    _activeTaxRate.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                    _activeTaxRate.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                    dbContext.TaxRates.Add(_activeTaxRate);
                    dbContext.SaveChanges();
                }
            }

            MessageBoxService.SaveConfirmation(_activeTaxRate.Title);
            LoadSearchGridControl();
            IsEditing();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeTaxRate.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var taxRateInDb = dbContext.TaxRates.Find(_activeTaxRate.Id);
                    dbContext.TaxRates.Remove(taxRateInDb);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeTaxRate = new TaxRate();
                IsNotEditingAndAdding();
            }
        }

        protected override void OnAdding()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeTaxRate.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInactiveHeaderTitle(UserControlTitle);
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void SearchGridControlOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var searchGridControl = (GridControl)sender;
            var rowIndex = searchGridControl.View.GetRowHandleByMouseEventArgs(e);
            if (rowIndex == DataControlBase.InvalidRowHandle)
                return;
            if (searchGridControl.ItemsSource is ObservableCollection<TaxRate> taxRates)
                EditData(taxRates[rowIndex]);
        }

        private void EditData(TaxRate taxRate)
        {
            _activeTaxRate = taxRate;
            FillData(taxRate);
            IsEditing();
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var taxRate = SearchGridControl.SelectedItem as TaxRate;
            EditData(taxRate);
        }

        private void FillData(TaxRate taxRate)
        {
            TitleTextEdit.Text = taxRate.Title;
            AbbreviationTextEdit.Text = taxRate.Abbreviation;
            DescriptionTextEdit.Text = taxRate.Description;
            if (taxRate.IsPercentageSelected)
            {
                PercentageRadioButton.IsChecked = true;
                PercentageSpinEdit.EditValue = taxRate.Percentage;
                FlatRateSpinEdit.EditValue = null;
            }
            else
            {
                FlatRateRadioButton.IsChecked = true;
                FlatRateSpinEdit.EditValue = taxRate.FlatRate;
                PercentageSpinEdit.EditValue = null;
            }

            IsDefaultCheckEdit.EditValue = taxRate.IsDefault;
            IsActiveCheckEdit.EditValue = taxRate.IsActive;
        }

        private void ReadData(TaxRate taxRate)
        {
            taxRate.Title = TitleTextEdit.Text;
            taxRate.Abbreviation = AbbreviationTextEdit.Text;
            taxRate.Description = DescriptionTextEdit.Text;
            if (PercentageRadioButton.IsChecked == true)
            {
                taxRate.IsPercentageSelected = true;
                taxRate.Percentage = Convert.ToDouble(PercentageSpinEdit.EditValue);
            }
            else
            {
                taxRate.IsPercentageSelected = false;
                taxRate.FlatRate = Convert.ToDouble(FlatRateSpinEdit.EditValue);
            }

            taxRate.IsDefault = Convert.ToBoolean(IsDefaultCheckEdit.EditValue);
            taxRate.IsActive = Convert.ToBoolean(IsActiveCheckEdit.EditValue);
        }

        private void PercentageRadioButtonOnChecked(object sender, RoutedEventArgs e)
        {
            PercentageSpinEdit.IsEnabled = true;
            FlatRateSpinEdit.IsEnabled = false;
        }

        private void FlatRateRadioButtonOnChecked(object sender, RoutedEventArgs e)
        {
            FlatRateSpinEdit.IsEnabled = true;
            PercentageSpinEdit.IsEnabled = false;
        }
    }
}
