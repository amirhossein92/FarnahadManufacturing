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
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcLocationGroup : FilterUserControlBase
    {
        private ObservableCollection<LocationGroup> _locationGroups;
        private LocationGroup _activeLocationGroup;

        public UcLocationGroup()
        {
            InitializeComponent();

            UserControlTitle = "گروه محل ها";
            ImagePath = "Icons/NavigationBar/LocationGroup_Small.svg";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchStatusComboBox();
            LoadSearchGridControl();

            LoadCategoryComboBox();
        }

        private void LoadSearchStatusComboBox()
        {
            var statuses = new List<FmComboModel<bool>>();
            statuses.Add(new FmComboModel<bool>(true, "فعال"));
            statuses.Add(new FmComboModel<bool>(false, "غیر فعال"));
            SearchStatusComboBoxEdit.ItemsSource = statuses;
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchStatusComboBoxEdit.EditValue);
        }

        private void LoadSearchGridControlData(string title, object statusComboValue)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var locationGroupsQueryable = dbContext.LocationGroups
                    .OrderBy(item => item.Id)
                    .Include(item => item.Users)
                    .AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    locationGroupsQueryable = locationGroupsQueryable.Where(item => item.Title.Contains(title));
                if (statusComboValue is bool status)
                    locationGroupsQueryable = locationGroupsQueryable.Where(item => item.IsActive == status);
                var totalRecordsCount = locationGroupsQueryable.Count();
                _locationGroups = locationGroupsQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _locationGroups;
                PaginationUserControl.UpdateRecordsDetail(_locationGroups.Count, totalRecordsCount);
            }
        }

        private void LoadCategoryComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var categories = dbContext.Categories
                    .Select(item => new FmComboModel<int> { Value = item.Id, Title = item.Title })
                    .ToList();
                CategoryComboBoxEdit.ItemsSource = categories;
            }
        }

        private List<User> GetAvailableUsers()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var users = dbContext.Users.ToList();
                if (_activeLocationGroup != null)
                {
                    foreach (var user in _activeLocationGroup.Users)
                    {
                        var userInDb = users.First(item => item.Id == user.Id);
                        users.Remove(userInDb);
                    }
                }

                return users;
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activeLocationGroup = new LocationGroup { IsActive = true };
            FillData(_activeLocationGroup);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeLocationGroup);
            if (_activeLocationGroup.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var locationGroupInDb = dbContext.LocationGroups
                        .Include(item => item.Users)
                        .First(item => item.Id == _activeLocationGroup.Id);
                    locationGroupInDb.Title = _activeLocationGroup.Title;
                    locationGroupInDb.CategoryId = _activeLocationGroup.CategoryId;
                    locationGroupInDb.IsActive = _activeLocationGroup.IsActive;

                    locationGroupInDb.Users.Clear();
                    foreach (var user in _activeLocationGroup.Users)
                    {
                        var userInDb = dbContext.Users.Find(user.Id);
                        locationGroupInDb.Users.Add(userInDb);
                    }

                    dbContext.SaveChanges();

                    IsEditing();
                }
            }
            else
            {
                _activeLocationGroup.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeLocationGroup.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    foreach (var user in _activeLocationGroup.Users)
                        dbContext.Users.Attach(user);
                    dbContext.LocationGroups.Add(_activeLocationGroup);
                    dbContext.SaveChanges();
                }

                OnAddToolBarItem();
            }

            MessageBoxService.SaveConfirmation(_activeLocationGroup.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeLocationGroup.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var locationGroupInDb = dbContext.LocationGroups.Find(_activeLocationGroup.Id);
                    dbContext.LocationGroups.Remove(locationGroupInDb);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeLocationGroup = new LocationGroup();
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
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeLocationGroup.Title);
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
            if (searchGridControl.ItemsSource is ObservableCollection<LocationGroup> locationGroups)
                EditData(locationGroups[rowIndex]);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var locationGroup = SearchGridControl.SelectedItem as LocationGroup;
            EditData(locationGroup);
        }

        private void ListBoxButtonsUserControlOnAddSingle(object sender, RoutedEventArgs e)
        {
            if (AvailableUsersListBox.SelectedItem is User selectedUser)
            {
                var tempUser = selectedUser;
                var availableUsers = AvailableUsersListBox.ItemsSource as ObservableCollection<User>;
                availableUsers.Remove(selectedUser);
                var currentUsers = CurrentUsersListBox.ItemsSource as ObservableCollection<User>;
                currentUsers.Add(tempUser);
            }
        }

        private void ListBoxButtonsUserControlOnRemoveSingle(object sender, RoutedEventArgs e)
        {
            if (CurrentUsersListBox.SelectedItem is User selectedUser)
            {
                var tempUser = selectedUser;
                var currentUsers = CurrentUsersListBox.ItemsSource as ObservableCollection<User>;
                currentUsers.Remove(selectedUser);
                var availableUsers = AvailableUsersListBox.ItemsSource as ObservableCollection<User>;
                if (!availableUsers.Any(item => item.Id == tempUser.Id))
                    availableUsers.Add(tempUser);
            }
        }

        public void EditData(LocationGroup locationGroup)
        {
            _activeLocationGroup = locationGroup;
            FillData(_activeLocationGroup);
            IsEditing();
        }

        public void FillData(LocationGroup locationGroup)
        {
            NameTextEdit.Text = locationGroup.Title;
            CategoryComboBoxEdit.EditValue = _activeLocationGroup.CategoryId;
            IsActiveCheckEdit.EditValue = _activeLocationGroup.IsActive;
            CurrentUsersListBox.ItemsSource = new ObservableCollection<User>(locationGroup.Users);
            AvailableUsersListBox.ItemsSource = new ObservableCollection<User>(GetAvailableUsers());

        }

        public void ReadData(LocationGroup locationGroup)
        {
            locationGroup.Title = NameTextEdit.Text;
            _activeLocationGroup.CategoryId = (int?)CategoryComboBoxEdit.EditValue;
            _activeLocationGroup.IsActive = (bool)IsActiveCheckEdit.EditValue;
            if (CurrentUsersListBox.ItemsSource is ObservableCollection<User> users)
                locationGroup.Users = users.ToList();
        }
    }
}
