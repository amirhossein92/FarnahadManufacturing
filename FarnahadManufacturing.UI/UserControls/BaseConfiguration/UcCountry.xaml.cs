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
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.UI.Base.UserControl;
using FarnahadManufacturing.UI.Common;

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
            ReadData(ref _activeCountry);
            if (_activeCountry.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeCountry).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
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
            }

            MessageBoxService.SaveConfirmation(_activeCountry.Title);
            LoadSearchGridControl();
            IsEditing();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeCountry.Title) == DialogResult.Yes)
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
                TotalRecordsCount = countryQueryable.Count();
                _countries = countryQueryable.Paginate(CurrentPage);
                SearchGridControl.ItemsSource = _countries;
                PaginationUserControl.RecordCountText =
                    PaginationUtility.GetRecordsDetailText(CurrentPage, _countries.Count, TotalRecordsCount);
            }
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
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

        private void PreviousPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadSearchGridControl();
            }
        }

        private void NextPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentPage <= (TotalRecordsCount / ApplicationSetting.PageRecordNumber))
            {
                CurrentPage++;
                LoadSearchGridControl();
            }
        }

        private void FillData(Country country)
        {
            NameTextEdit.Text = country.Title;
            DescriptionTextEdit.Text = country.Description;
        }

        private void ReadData(ref Country country)
        {
            country.Title = NameTextEdit.Text;
            country.Description = DescriptionTextEdit.Text;
        }

        protected override void OnAdding()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeCountry.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInActiveHeaderTitle(UserControlTitle);
        }
    }
}
