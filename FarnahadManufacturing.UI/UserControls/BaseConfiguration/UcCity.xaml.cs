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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.UI.Base.UserControl;
using FarnahadManufacturing.UI.Common;
using MessageBox = System.Windows.Forms.MessageBox;

namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcCity : UserControlBase
    {
        private ObservableCollection<City> _cities;
        private City _activeCity;
        private int _currentPage = 1;
        private int _totalRecordsCount;
        private string _userControlTitle = "شهر";

        public UcCity()
        {
            InitializeComponent();
            Loaded += OnLoaded;

            _cities = new ObservableCollection<City>();
            LoadToolBarItems();
            LoadSearchGridControlData();
            LoadCountryComboBox();
            IsNotEditingAndAdding();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void LoadCountryComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var countries = dbContext.Countries.Select(item => new { Title = item.Title, Id = item.Id }).ToList();
                CountryComboBox.ItemsSource = countries;
                CountryComboBox.DisplayMember = "Title";
                CountryComboBox.ValueMember = "Id";
            }
        }

        private void LoadSearchGridControlData(string searchTitle = null, string searchCountryTitle = null)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var cityQueryable = dbContext.Cities.OrderBy(item => item.Id).Include(item => item.Country).AsQueryable();
                if (!string.IsNullOrEmpty(searchTitle))
                    cityQueryable = cityQueryable.Where(item => item.Title.Contains(searchTitle));
                if (!string.IsNullOrEmpty(searchCountryTitle))
                    cityQueryable = cityQueryable.Where(item => item.Country.Title.Contains(searchCountryTitle));
                _totalRecordsCount = cityQueryable.Count();
                _cities = cityQueryable.Paginate(_currentPage);
                SearchGridControl.ItemsSource = _cities;
                RecordsCountFmLabel.Text =
                    PaginationUtility.GetRecordsDetailText(_currentPage, _cities.Count, _totalRecordsCount);

            }
        }

        private void LoadToolBarItems()
        {
            var addButton = new BarButtonItem
            {
                Name = "Add",
                Content = "اضافه",
                Glyph = new BitmapImage(new Uri("Assets/AccordionIcons/Icon_32x32.png", UriKind.Relative)),
            };
            addButton.ItemClick += AddButtonOnItemClick;
            var saveButton = new BarButtonItem
            {
                Name = "Save",
                Content = "ذخیره",
                Glyph = new BitmapImage(new Uri("Assets/AccordionIcons/Icon_32x32.png", UriKind.Relative)),

            };
            saveButton.ItemClick += SaveButtonOnItemClick;
            var deleteButton = new BarButtonItem
            {
                Name = "Delete",
                Content = "حذف",
                Glyph = new BitmapImage(new Uri("Assets/AccordionIcons/Icon_32x32.png", UriKind.Relative)),

            };
            deleteButton.ItemClick += DeleteButtonOnItemClick;
            ToolBarItems.Add(addButton.Name, addButton);
            ToolBarItems.Add(saveButton.Name, saveButton);
            ToolBarItems.Add(deleteButton.Name, deleteButton);
        }

        private void AddButtonOnItemClick(object sender, ItemClickEventArgs e)
        {
            _activeCity = new City();
            EditData(_activeCity);
            IsAdding();
        }

        private void SaveButtonOnItemClick(object sender, ItemClickEventArgs e)
        {
            ReadData(ref _activeCity);
            if (_activeCity.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeCity).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            else
            {
                _activeCity.CreatedByUserId = 3;
                _activeCity.CreatedDateTime = DateTime.Now;
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Cities.Add(_activeCity);
                    dbContext.SaveChanges();
                }
            }

            MessageBox.Show("ذخیره با موفقیت انجام شد", "ذخیره", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchCountryTextEdit.Text);
            IsEditing();
        }

        private void DeleteButtonOnItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageBox.Show("آیا از حذف شدن اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var city = dbContext.Cities.Find(_activeCity.Id);
                    dbContext.Cities.Remove(city);
                    dbContext.SaveChanges();
                }
                LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchCountryTextEdit.Text);
                _activeCity = new City();
            }
            IsNotEditingAndAdding();
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchCountryTextEdit.Text);
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
            MainLayoutGroup.IsEnabled = true;
            _activeCity = city;
            FillData(_activeCity);
            IsEditing();
        }

        private void FillData(City city)
        {
            TitleTextEdit.Text = city.Title;
            CountryComboBox.EditValue = city.CountryId;
            DescriptionTextEdit.Text = city.Description;
        }

        private void ReadData(ref City city)
        {
            city.Title = TitleTextEdit.Text;
            city.CountryId = Convert.ToInt32(CountryComboBox.EditValue);
            city.Description = DescriptionTextEdit.Text;
        }

        private void PreviousPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchCountryTextEdit.Text);
            }
        }

        private void NextPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_currentPage <= (_totalRecordsCount / ApplicationSetting.PageRecordNumber))
            {
                _currentPage++;
                LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchCountryTextEdit.Text);
            }
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var city = SearchGridControl.SelectedItem as City;
            EditData(city);
        }

        private void IsAdding()
        {
            MainLayoutGroup.IsEnabled = true;
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Add", true);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Save", true);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Delete", false);
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(_userControlTitle);
        }

        private void IsEditing()
        {
            MainLayoutGroup.IsEnabled = true;
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Add", true);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Save", true);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Delete", true);
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateEditHeaderTitle(_userControlTitle, _activeCity.Title);
        }

        private void IsNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Add", true);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Save", false);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Delete", false);
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInActiveHeaderTitle(_userControlTitle);
        }
    }
}
