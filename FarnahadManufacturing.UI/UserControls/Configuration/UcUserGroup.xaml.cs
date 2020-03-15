using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcUserGroup : FilterUserControlBase
    {
        private ObservableCollection<UserGroup> _userGroups;
        private UserGroup _activeUserGroup;

        public UcUserGroup()
        {
            InitializeComponent();

            UserControlTitle = "گروه کاربر";
            ImagePath = "Icons/NavigationBar/UserGroup_Small.svg";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchGridControl();
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text);
        }

        private void LoadSearchGridControlData(string title)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var userGroupQueryable = dbContext.UsersGroups
                    .Include(item => item.MembersUsers)
                    .OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    userGroupQueryable = userGroupQueryable.Where(item => item.Title.Contains(title));
                var totalRecordsCount = userGroupQueryable.Count();
                _userGroups = userGroupQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _userGroups;
                PaginationUserControl.UpdateRecordsDetail(_userGroups.Count, totalRecordsCount);
            }
        }

        private List<User> GetAvailableUsers()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var users = dbContext.Users.ToList();
                if (_activeUserGroup != null)
                {
                    foreach (var user in _activeUserGroup.MembersUsers)
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
            _activeUserGroup = new UserGroup();
            FillData(_activeUserGroup);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeUserGroup);
            if (_activeUserGroup.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var userGroupInDb = dbContext.UsersGroups
                        .Include(item => item.MembersUsers)
                        .First(item => item.Id == _activeUserGroup.Id);
                    userGroupInDb.Title = _activeUserGroup.Title;

                    userGroupInDb.MembersUsers.Clear();
                    foreach (var user in _activeUserGroup.MembersUsers)
                    {
                        var userInDb = dbContext.Users.Find(user.Id);
                        userGroupInDb.MembersUsers.Add(userInDb);
                    }

                    dbContext.SaveChanges();

                    IsEditing();
                }
            }
            else
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    _activeUserGroup.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                    _activeUserGroup.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                    foreach (var user in _activeUserGroup.MembersUsers)
                        dbContext.Users.Attach(user);
                    dbContext.UsersGroups.Add(_activeUserGroup);
                    dbContext.SaveChanges();

                    OnAddToolBarItem();
                }
            }

            MessageBoxService.SaveConfirmation(_activeUserGroup.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeUserGroup.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var userGroupInDb = dbContext.UsersGroups.Find(_activeUserGroup.Id);
                    dbContext.UsersGroups.Remove(userGroupInDb);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeUserGroup = new UserGroup();
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
            TitleTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeUserGroup.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            TitleTextEdit.Focus();
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
            if (searchGridControl.ItemsSource is ObservableCollection<UserGroup> userGroups)
                EditData(userGroups[rowIndex]);
        }

        private void EditData(UserGroup userGroup)
        {
            _activeUserGroup = userGroup;
            FillData(_activeUserGroup);
            IsEditing();
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var userGroup = SearchGridControl.SelectedItem as UserGroup;
            EditData(userGroup);
        }

        private void FillData(UserGroup userGroup)
        {
            TitleTextEdit.Text = userGroup.Title;
            CurrentUsersListBoxEdit.ItemsSource = new ObservableCollection<User>(userGroup.MembersUsers);
            AvailableUsersListBoxEdit.ItemsSource = new ObservableCollection<User>(GetAvailableUsers());
        }

        private void ReadData(UserGroup userGroup)
        {
            userGroup.Title = TitleTextEdit.Text;
            if (CurrentUsersListBoxEdit.ItemsSource is ObservableCollection<User> users)
                userGroup.MembersUsers = users.ToList();
        }

        private void ListBoxButtonsUserControlOnAddSingle(object sender, RoutedEventArgs e)
        {
            if (AvailableUsersListBoxEdit.SelectedItem is User selectedUser)
            {
                var tempUser = selectedUser;
                var availableUsers = AvailableUsersListBoxEdit.ItemsSource as ObservableCollection<User>;
                availableUsers.Remove(selectedUser);
                var currentUsers = CurrentUsersListBoxEdit.ItemsSource as ObservableCollection<User>;
                currentUsers.Add(tempUser);
            }
        }

        private void ListBoxButtonsUserControlOnRemoveSingle(object sender, RoutedEventArgs e)
        {
            if (CurrentUsersListBoxEdit.SelectedItem is User selectedUser)
            {
                var tempUser = selectedUser;
                var currentUsers = CurrentUsersListBoxEdit.ItemsSource as ObservableCollection<User>;
                currentUsers.Remove(selectedUser);
                var availableUsers = AvailableUsersListBoxEdit.ItemsSource as ObservableCollection<User>;
                if (!availableUsers.Any(item => item.Id == tempUser.Id))
                    availableUsers.Add(tempUser);
            }
        }
    }
}
