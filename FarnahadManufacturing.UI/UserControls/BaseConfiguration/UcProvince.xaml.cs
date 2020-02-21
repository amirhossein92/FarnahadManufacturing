using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.EditForm;
using DevExpress.XtraPrinting.Native;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.UI.Common;

namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcProvince : FilterUserControlBase
    {
        private ObservableCollection<Province> _provinces;
        private Province _activeProvince;

        public UcProvince()
        {
            InitializeComponent();

            UserControlTitle = "استان";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchGridControl();

            LoadCountryComboBox();
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchCountryTextEdit.Text);
        }

        private void LoadSearchGridControlData(string title, string country)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var provincesQueryable = dbContext.Provinces.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    provincesQueryable = provincesQueryable.Where(item => item.Title.Contains(title) ||
                                                                          item.Abbreviation.Contains(title));
                if (!string.IsNullOrEmpty(country))
                    provincesQueryable = provincesQueryable.Where(item => item.Country.Title.Contains(country) ||
                                                                          item.Country.Abbreviation.Contains(country));
                var totalRecordsCount = provincesQueryable.Count();
                _provinces = provincesQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _provinces;
                PaginationUserControl.UpdateRecordsDetail( _provinces.Count, totalRecordsCount);
            }
        }

        private void LoadCountryComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var countries = dbContext.Countries.Select(item => new FmComboModel<int>
                {
                    Value = item.Id,
                    Title = item.Title,
                }).ToList();
                CountryComboBoxEdit.ItemsSource = countries;
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activeProvince = new Province();
            FillData(_activeProvince);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeProvince);
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                if (_activeProvince.Id > 0)
                {
                    dbContext.SaveChanges();
                }
                else
                {
                    _activeProvince.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                    _activeProvince.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                    dbContext.Provinces.Add(_activeProvince);
                    dbContext.SaveChanges();
                }
            }

            MessageBoxService.SaveConfirmation(_activeProvince.Title);
            LoadSearchGridControl();
            IsEditing();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeProvince.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var province = dbContext.Provinces.Find(_activeProvince.Id);
                    dbContext.Provinces.Remove(province);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeProvince = new Province();
                IsNotEditingAndAdding();
            }
        }

        protected override void OnAdding()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeProvince.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateInactiveHeaderTitle(UserControlTitle);
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
            if (searchGridControl.ItemsSource is ObservableCollection<Province> provinces)
                EditData(provinces[rowIndex]);
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var province = SearchGridControl.SelectedItem as Province;
            EditData(province);
        }

        private void EditData(Province province)
        {
            _activeProvince = province;
            FillData(_activeProvince);
            IsEditing();
        }

        private void FillData(Province province)
        {
            NameTextEdit.Text = province.Title;
            AbbreviationTextEdit.Text = province.Abbreviation;
            CountryComboBoxEdit.EditValue = province.CountryId;
            DescriptionTextEdit.Text = province.Description;
        }

        private void ReadData(Province province)
        {
            province.Title = NameTextEdit.Text;
            province.Abbreviation = AbbreviationTextEdit.Text;
            province.CountryId = Convert.ToInt32(CountryComboBoxEdit.EditValue);
            province.Description = DescriptionTextEdit.Text;
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }
    }
}
