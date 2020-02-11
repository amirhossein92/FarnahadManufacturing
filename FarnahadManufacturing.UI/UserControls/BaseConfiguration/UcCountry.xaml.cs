using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.LayoutControl;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.UI.Base.UserControl;
using FarnahadManufacturing.UI.Common;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcCountry : UserControlBase
    {
        private ObservableCollection<Country> _countries;
        private Country _activeCountry;
        private int _currentPage = 1;
        private int _totalRecordsCount;
        private string _userControlTitle = "کشور";

        public UcCountry()
        {
            InitializeComponent();
            this.Loaded += UserControlOnLoaded;

            _countries = new ObservableCollection<Country>();
            LoadToolBarItems();
        }

        private void UserControlOnLoaded(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControlData();
            IsNotEditingAndAdding();
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
            _activeCountry = new Country();
            FillData(_activeCountry);
            IsAdding();
        }

        private void SaveButtonOnItemClick(object sender, ItemClickEventArgs e)
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
                _activeCountry.CreatedByUserId = 3;
                _activeCountry.CreatedDateTime = DateTime.Now;
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Countries.Add(_activeCountry);
                    dbContext.SaveChanges();
                }
            }

            MessageBox.Show("ذخیره با موفقیت انجام شد", "ذخیره", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadSearchGridControlData(SearchTitleTextEdit.Text);
            IsEditing();
        }

        private void DeleteButtonOnItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageBox.Show("آیا از حذف شدن اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var country = dbContext.Countries.Find(_activeCountry.Id);
                    dbContext.Countries.Remove(country);
                    dbContext.SaveChanges();
                }
                LoadSearchGridControlData(SearchTitleTextEdit.Text);
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
                _totalRecordsCount = countryQueryable.Count();
                _countries = countryQueryable.Paginate(_currentPage);
                SearchGridControl.ItemsSource = _countries;
                RecordsCountFmLabel.Text =
                    PaginationUtility.GetRecordsDetailText(_currentPage, _countries.Count, _totalRecordsCount);
            }
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text);
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
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadSearchGridControlData(SearchTitleTextEdit.Text);
            }
        }

        private void NextPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_currentPage <= (_totalRecordsCount / ApplicationSetting.PageRecordNumber))
            {
                _currentPage++;
                LoadSearchGridControlData(SearchTitleTextEdit.Text);
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
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateEditHeaderTitle(_userControlTitle, _activeCountry.Title);
        }

        private void IsNotEditingAndAdding()
        {
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Add", true);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Save", false);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Delete", false);
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInActiveHeaderTitle(_userControlTitle);
        }
    }
}
