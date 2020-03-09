using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Utils.Svg;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.LayoutControl;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcCountry : FilterUserControlBase
    {
        private ObservableCollection<Country> _countries;
        private Country _activeCountry;

        public UcCountry()
        {
            InitializeComponent();
            this.Loaded += UserControlOnLoaded;

            UserControlTitle = "کشور";
            InitialData();
        }

        private void UserControlOnLoaded(object sender, RoutedEventArgs e)
        {
        }

        protected sealed override void InitialData()
        {
            _countries = new ObservableCollection<Country>();
            LoadSearchGridControlData();
            IsNotEditingAndAdding();
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text);
        }

        protected override void OnAddToolBarItem()
        {
            _activeCountry = new Country();
            FillData(_activeCountry);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeCountry);
            if (_activeCountry.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeCountry).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

                IsEditing();
            }
            else
            {
                _activeCountry.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeCountry.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Countries.Add(_activeCountry);
                    dbContext.SaveChanges();
                }

                OnAddToolBarItem();
            }

            MessageBoxService.SaveConfirmation(_activeCountry.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeCountry.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var country = dbContext.Countries.Find(_activeCountry.Id);
                    dbContext.Countries.Remove(country);
                    dbContext.SaveChanges();
                }
                LoadSearchGridControl();
                _activeCountry = new Country();
                MainLayoutGroup.IsEnabled = false;
            }
            IsNotEditingAndAdding();
        }

        private void LoadSearchGridControlData(string searchTitle = null)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var countryQueryable = dbContext.Countries.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(searchTitle))
                    countryQueryable = countryQueryable.Where(item => item.Title.Contains(searchTitle) ||
                                                                      item.Description.Contains(searchTitle));
                var totalRecordsCount = countryQueryable.Count();
                _countries = countryQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _countries;
                PaginationUserControl.UpdateRecordsDetail(_countries.Count, totalRecordsCount);
            }
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            PaginationUserControl.CurrentPage = 1;
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var country = SearchGridControl.SelectedItem as Country;
            EditData(country);
        }

        private void SearchGridControlOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var searchGridControl = (GridControl)sender;
            var rowIndex = searchGridControl.View.GetRowHandleByMouseEventArgs(e);
            if (rowIndex == DataControlBase.InvalidRowHandle)
                return;
            if (searchGridControl.ItemsSource is ObservableCollection<Country> countries)
                EditData(countries[rowIndex]);
        }

        private void EditData(Country country)
        {
            _activeCountry = country;
            FillData(_activeCountry);
            IsEditing();
        }

        private void FillData(Country country)
        {
            NameTextEdit.Text = country.Title;
            AbbreviationTextEdit.Text = country.Abbreviation;
            DescriptionTextEdit.Text = country.Description;
        }

        private void ReadData(Country country)
        {
            country.Title = NameTextEdit.Text;
            country.Abbreviation = AbbreviationTextEdit.Text;
            country.Description = DescriptionTextEdit.Text;
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
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeCountry.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInactiveHeaderTitle(UserControlTitle);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }
    }
}
