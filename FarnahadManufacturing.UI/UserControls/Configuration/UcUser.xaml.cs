using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.UI.Common;
using MessageBox = System.Windows.MessageBox;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcUser : FilterUserControlBase
    {
        private ObservableCollection<User> _users;
        private User _activeUser;

        public UcUser()
        {
            InitializeComponent();

            UserControlTitle = "کاربر";
            ImagePath = "Icons/NavigationBar/User_Small.svg";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            _users = new ObservableCollection<User>();
            LoadSearchGridControlData();
            LoadCustomerGroups();
        }

        private void LoadSearchGridControlData(string userName = null, string name = null)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var usersQueryable = dbContext.Users
                    .OrderBy(item => item.Id)
                    .Include(item => item.LocationGroupMembers).AsQueryable();
                if (!string.IsNullOrEmpty(userName))
                    usersQueryable = usersQueryable.Where(item => item.UserName.Contains(userName));
                if (!string.IsNullOrEmpty(name))
                    usersQueryable = usersQueryable.Where(item => item.LastName.Contains(name) ||
                                                                  item.LastName.Contains(name));
                var totalRecordsCount = usersQueryable.Count();
                _users = usersQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _users;
                PaginationUserControl.UpdateRecordsDetail(_users.Count, totalRecordsCount);
            }
        }

        private void LoadCustomerGroups()
        {
        }

        private List<LocationGroup> GetAvailableLocationGroups()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var locationGroups = dbContext.LocationGroups.ToList();
                if (_activeUser != null)
                {
                    foreach (var locationGroup in _activeUser.LocationGroupMembers)
                    {
                        var locationGroupInDb = locationGroups.First(item => item.Id == locationGroup.Id);
                        locationGroups.Remove(locationGroupInDb);
                    }
                }

                return locationGroups;
            }
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchUserNameTextEdit.Text, SearchNameTextEdit.Text);
        }

        protected override void OnAddToolBarItem()
        {
            _activeUser = new User { IsActive = true };
            FillData(_activeUser);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeUser);
            if (!string.IsNullOrEmpty(_activeUser.Password))
            {
                if (_activeUser.Id > 0)
                {
                    using (var dbContext = new FarnahadManufacturingDbContext())
                    {
                        var userInDb = dbContext.Users
                            .Include(item => item.LocationGroupMembers)
                            .First(item => item.Id == _activeUser.Id);

                        userInDb.FirstName = _activeUser.FirstName;
                        userInDb.LastName = _activeUser.LastName;
                        userInDb.UserName = _activeUser.UserName;
                        userInDb.Email = _activeUser.Email;
                        userInDb.PhoneNumber = _activeUser.PhoneNumber;
                        userInDb.Initial = _activeUser.Initial;
                        userInDb.IsActive = _activeUser.IsActive;
                        userInDb.PasswordSalt = _activeUser.PasswordSalt;
                        userInDb.Password = _activeUser.Password;

                        userInDb.LocationGroupMembers.Clear();
                        foreach (var locationGroup in _activeUser.LocationGroupMembers)
                        {
                            var locationGroupInDb = dbContext.LocationGroups.Find(locationGroup.Id);
                            userInDb.LocationGroupMembers.Add(locationGroupInDb);
                        }

                        dbContext.SaveChanges();

                        IsEditing();
                    }
                }
                else
                {
                    _activeUser.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                    using (var dbContext = new FarnahadManufacturingDbContext())
                    {
                        dbContext.Users.Add(_activeUser);
                        foreach (var locationGroup in _activeUser.LocationGroupMembers)
                        {
                            locationGroup.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                            locationGroup.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                        }
                        dbContext.SaveChanges();

                        OnAddToolBarItem();
                    }
                }
            }
            else
            {
                MessageBox.Show("رمز عبور باید تنظیم شود");
            }

            MessageBoxService.SaveConfirmation(_activeUser.UserName);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeUser.UserName) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var user = dbContext.Users.Find(_activeUser.Id);
                    dbContext.Users.Remove(user);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeUser = new User();
                IsNotEditingAndAdding();
            }

        }

        protected override void OnAdding()
        {
            FirstNameTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            FirstNameTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeUser.UserName);
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
            if (searchGridControl.ItemsSource is ObservableCollection<User> users)
                EditData(users[rowIndex]);
        }

        private void EditData(User user)
        {
            _activeUser = user;
            FillData(_activeUser);
            IsEditing();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var user = SearchGridControl.SelectedItem as User;
            EditData(user);
        }

        private void SetPasswordOnClick(object sender, RoutedEventArgs e)
        {
            WindowService.OpenUserControlDialog(new UcSetPassword(_activeUser.Id));
            _activeUser.Password = ApplicationDataStore.GetData<string>("CurrentPassword");
            _activeUser.PasswordSalt = ApplicationDataStore.GetData<string>("CurrentPasswordSalt");
        }

        private void ReadData(User user)
        {
            user.FirstName = FirstNameTextEdit.Text;
            user.LastName = LastNameTextEdit.Text;
            user.UserName = UserNameTextEdit.Text;
            user.Initial = InitialTextEdit.Text;
            user.Email = EmailTextEdit.Text;
            user.PhoneNumber = PhoneNumberTextEdit.Text;
            //user.CustomerGroups = CurrentUserGroupsListBoxEdit.ItemsSource as ObservableCollection<CustomerGroup>;
            if (CurrentLocationGroupListBoxEdit.ItemsSource is ObservableCollection<LocationGroup> currentLocationGroups)
                user.LocationGroupMembers = currentLocationGroups.ToList();
        }

        private void FillData(User user)
        {
            FirstNameTextEdit.Text = user.FirstName;
            LastNameTextEdit.Text = user.LastName;
            UserNameTextEdit.Text = user.UserName;
            InitialTextEdit.Text = user.Initial;
            EmailTextEdit.Text = user.Email;
            PhoneNumberTextEdit.Text = user.PhoneNumber;
            CurrentUserGroupsListBoxEdit.ItemsSource = new ObservableCollection<CustomerGroup>(user.CustomerGroups);
            CurrentLocationGroupListBoxEdit.ItemsSource = new ObservableCollection<LocationGroup>(user.LocationGroupMembers);
            AvailableLocationGroupListBoxEdit.ItemsSource = new ObservableCollection<LocationGroup>(GetAvailableLocationGroups());
        }

        private void AddSelectedLocationGroupToCurrentLocationGroupsOnClick(object sender, RoutedEventArgs e)
        {
            if (AvailableLocationGroupListBoxEdit.SelectedItem is LocationGroup selectedLocationGroup)
            {
                var tempLocationGroup = selectedLocationGroup;
                var availableLocationGroups = AvailableLocationGroupListBoxEdit.ItemsSource as ObservableCollection<LocationGroup>;
                availableLocationGroups.Remove(selectedLocationGroup);
                var currentLocationGroups = CurrentLocationGroupListBoxEdit.ItemsSource as ObservableCollection<LocationGroup>;
                currentLocationGroups.Add(tempLocationGroup);
            }
        }

        private void RemoveSelectedLocationGroupFromCurrentLocationGroupsOnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentLocationGroupListBoxEdit.SelectedItem is LocationGroup selectedLocationGroup)
            {
                var tempLocationGroup = selectedLocationGroup;
                var currentLocationGroups = CurrentLocationGroupListBoxEdit.ItemsSource as ObservableCollection<LocationGroup>;
                currentLocationGroups.Remove(selectedLocationGroup);
                var availableLocationGroups = AvailableLocationGroupListBoxEdit.ItemsSource as ObservableCollection<LocationGroup>;
                if (!availableLocationGroups.Any(item => item.Id == tempLocationGroup.Id))
                    availableLocationGroups.Add(tempLocationGroup);
            }
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }
    }
}
