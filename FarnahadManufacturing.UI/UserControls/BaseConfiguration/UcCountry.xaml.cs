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
using FarnahadManufacturing.UI.Common;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    /// <summary>
    /// Interaction logic for UcCountry.xaml
    /// </summary>
    public partial class UcCountry : UserControl
    {

        private ObservableCollection<Country> _countries;
        private Country _activeCountry;
        private const int PageRecordNumber = 10;
        private int _currentPage = 1;
        private int _totalRecordsCount;

        public UcCountry()
        {
            InitializeComponent();
            this.Loaded += UserControlOnLoaded;

            _countries = new ObservableCollection<Country>();
            UpdateToolBar();
        }

        private void UserControlOnLoaded(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControlData();
            IsNotEditingAndAdding();
        }

        private void UpdateToolBar()
        {
            var toolBarItems = new Dictionary<string, IBarItem>();
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
            toolBarItems.Add(addButton.Name, addButton);
            toolBarItems.Add(saveButton.Name, saveButton);
            toolBarItems.Add(deleteButton.Name, deleteButton);

            ActiveToolBarService.AddBarItems(toolBarItems);
        }

        private void AddButtonOnItemClick(object sender, ItemClickEventArgs e)
        {
            MainLayoutGroup.IsEnabled = true;
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
                var countries = countryQueryable.Skip((_currentPage - 1) * PageRecordNumber).Take(PageRecordNumber)
                    .ToList();

                _countries = new ObservableCollection<Country>(countries);
                SearchGridControl.ItemsSource = _countries;
                var fromRecordNumber = (_currentPage - 1) * PageRecordNumber + 1;
                var toRecordNumber = fromRecordNumber + countries.Count - 1;
                RecordsCountFmLabel.Text = $"{fromRecordNumber}-{toRecordNumber} از {_totalRecordsCount}";
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
            MainLayoutGroup.IsEnabled = true;
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
            if (_currentPage <= (_totalRecordsCount / PageRecordNumber))
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
            ActiveToolBarService.ChangeToolBarItemStatus("Add", true);
            ActiveToolBarService.ChangeToolBarItemStatus("Save", true);
            ActiveToolBarService.ChangeToolBarItemStatus("Delete", false);
        }

        private void IsEditing()
        {
            ActiveToolBarService.ChangeToolBarItemStatus("Add", true);
            ActiveToolBarService.ChangeToolBarItemStatus("Save", true);
            ActiveToolBarService.ChangeToolBarItemStatus("Delete", true);
        }

        private void IsNotEditingAndAdding()
        {
            ActiveToolBarService.ChangeToolBarItemStatus("Add", true);
            ActiveToolBarService.ChangeToolBarItemStatus("Save", false);
            ActiveToolBarService.ChangeToolBarItemStatus("Delete", false);
        }
    }
}
