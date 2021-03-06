﻿using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcCity : FilterUserControlPage
    {
        private ObservableCollection<City> _cities;
        private City _activeCity;

        public UcCity()
        {
            InitializeComponent();
            Loaded += OnLoaded;

            UserControlTitle = "شهر";
            ImagePath = "Icons/NavigationBar/City_Small.svg";
            InitialData();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadCountryComboBox();
            LoadProvinceComboBox(Convert.ToInt32(CountryComboBox.EditValue));
            CountryComboBox.SelectedIndexChanged += CountryComboBoxOnSelectedIndexChanged;
        }

        protected sealed override void InitialData()
        {
            _cities = new ObservableCollection<City>();
            LoadSearchGridControlData();
            //LoadCountryComboBox();
            IsNotEditingAndAdding();
        }

        private void LoadCountryComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var countries = dbContext.Countries.Select(item => new { Title = item.Title, Id = item.Id }).ToList();
                CountryComboBox.ItemsSource = countries;
            }
        }

        private void CountryComboBoxOnSelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            var countryId = Convert.ToInt32(CountryComboBox.EditValue);
            LoadProvinceComboBox(countryId);
        }

        private void LoadProvinceComboBox(int countryId)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var provinces = dbContext.Provinces.Where(item => item.CountryId == countryId)
                    .Select(item => new { Title = item.Title, Id = item.Id }).ToList();
                ProvinceComboBox.ItemsSource = provinces;
            }
        }

        private void LoadSearchGridControlData(string searchTitle = null, string searchProvinceTitle = null, string searchCountryTitle = null)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var cityQueryable = dbContext.Cities.OrderBy(item => item.Id).Include(item => item.Province.Country).AsQueryable();
                if (!string.IsNullOrEmpty(searchTitle))
                    cityQueryable = cityQueryable.Where(item => item.Title.Contains(searchTitle));
                if (!string.IsNullOrEmpty(searchProvinceTitle))
                    cityQueryable = cityQueryable.Where(item => item.Province.Title.Contains(searchProvinceTitle));
                if (!string.IsNullOrEmpty(searchCountryTitle))
                    cityQueryable = cityQueryable.Where(item => item.Province.Country.Title.Contains(searchCountryTitle));
                var totalRecordsCount = cityQueryable.Count();
                _cities = cityQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _cities;
                PaginationUserControl.UpdateRecordsDetail(_cities.Count, totalRecordsCount);
            }
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchProvinceTextEdit.Text, SearchCountryTextEdit.Text);
        }

        protected override void OnAddToolBarItem()
        {
            _activeCity = new City();
            EditData(_activeCity);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeCity);
            if (_activeCity.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeCity).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

                IsEditing();
            }
            else
            {
                _activeCity.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeCity.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Cities.Add(_activeCity);
                    dbContext.SaveChanges();
                }

                OnAddToolBarItem();
            }

            MessageBoxService.SaveConfirmation(_activeCity.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeCity.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var city = dbContext.Cities.Find(_activeCity.Id);
                    dbContext.Cities.Remove(city);
                    dbContext.SaveChanges();
                }
                LoadSearchGridControl();
                _activeCity = new City();
            }
            IsNotEditingAndAdding();
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
            if (searchGridControl.ItemsSource is ObservableCollection<City> cities)
                EditData(cities[rowIndex]);
        }

        private void EditData(City city)
        {
            _activeCity = city;
            FillData(_activeCity);
            IsEditing();
        }

        private void FillData(City city)
        {
            TitleTextEdit.Text = city.Title;
            CountryComboBox.EditValue = city.Province?.CountryId;
            ProvinceComboBox.EditValue = city.Province?.Id;
            DescriptionTextEdit.Text = city.Description;
        }

        private void ReadData(City city)
        {
            city.Title = TitleTextEdit.Text;
            city.ProvinceId = Convert.ToInt32(ProvinceComboBox.EditValue);
            city.Description = DescriptionTextEdit.Text;
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var city = SearchGridControl.SelectedItem as City;
            EditData(city);
        }

        protected override void OnAdding()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
            TitleTextEdit.Focus();
        }

        protected override void OnEditing()
        {
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeCity.Title);
            TitleTextEdit.Focus();
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
