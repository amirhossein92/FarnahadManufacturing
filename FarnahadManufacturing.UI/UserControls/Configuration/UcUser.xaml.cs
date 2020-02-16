﻿using System;
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
using DevExpress.Xpf.Grid;
using DevExpress.XtraPrinting.Native;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Base.UserControl;
using FarnahadManufacturing.UI.Common;
using MessageBox = System.Windows.MessageBox;

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
            InitialData();
        }

        protected sealed override void InitialData()
        {
            _users = new ObservableCollection<User>();
            LoadSearchGridControlData();
            LoadCustomerGroups();
            LoadLocationGroups();
        }

        private void LoadSearchGridControlData(string userName = null, string name = null)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var usersQueryable = dbContext.Users.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(userName))
                    usersQueryable = usersQueryable.Where(item => item.UserName.Contains(userName));
                if (!string.IsNullOrEmpty(name))
                    usersQueryable = usersQueryable.Where(item => item.LastName.Contains(name) ||
                                                                  item.LastName.Contains(name));
                TotalRecordsCount = usersQueryable.Count();
                _users = usersQueryable.Paginate(CurrentPage);
                SearchGridControl.ItemsSource = _users;
                PaginationUserControl.RecordCountText = PaginationUtility.GetRecordsDetailText(CurrentPage, _users.Count, TotalRecordsCount);
            }
        }

        private void LoadCustomerGroups()
        {
        }

        private void LoadLocationGroups()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                AvailableLocationGroupListBoxEdit.ItemsSource =
                    new ObservableCollection<LocationGroup>(dbContext.LocationGroups.ToList());
            }
            CurrentLocationGroupListBoxEdit.ItemsSource = new ObservableCollection<LocationGroup>();
        }

        protected override void OnAddToolBarItem()
        {
            _activeUser = new User();
            FillData(_activeUser);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(ref _activeUser);
            if (!string.IsNullOrEmpty(_activeUser.Password))
            {
                if (_activeUser.Id > 0)
                {
                    using (var dbContext = new FarnahadManufacturingDbContext())
                    {
                        dbContext.Entry(_activeUser).State = EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                    _activeUser.CreatedDateTime = DateTime.Now;
                    using (var dbContext = new FarnahadManufacturingDbContext())
                    {
                        dbContext.Users.Add(_activeUser);
                        foreach (var locationGroup in _activeUser.LocationGroupMembers)
                        {
                            locationGroup.CreatedByUserId = 3;
                            locationGroup.CreatedDateTime = DateTime.Now;
                        }
                        dbContext.SaveChanges();
                    }
                }
            }
            else
            {
                MessageBox.Show("رمز عبور باید تنظیم شود");
            }

            MessageBoxService.SaveConfirmation(_activeUser.UserName);
            LoadSearchGridControlData(SearchUserNameTextEdit.Text, SearchNameTextEdit.Text);
            IsEditing();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeUser.UserName) == DialogResult.Yes)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var user = dbContext.Users.Find(_activeUser.Id);
                    dbContext.Users.Remove(user);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControlData(SearchUserNameTextEdit.Text, SearchNameTextEdit.Text);
                _activeUser = new User();
            }

            IsNotEditingAndAdding();
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
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeUser.UserName);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInActiveHeaderTitle(UserControlTitle);
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControlData(SearchUserNameTextEdit.Text, SearchNameTextEdit.Text);
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
            LoadLocationGroups();
            IsEditing();
        }

        private void PreviousPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadSearchGridControlData(SearchUserNameTextEdit.Text, SearchNameTextEdit.Text);
            }
        }

        private void NextPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentPage <= PaginationUtility.MaximumPageNumber(TotalRecordsCount))
            {
                CurrentPage++;
                LoadSearchGridControlData(SearchUserNameTextEdit.Text, SearchNameTextEdit.Text);
            }
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

        private void ReadData(ref User user)
        {
            user.FirstName = FirstNameTextEdit.Text;
            user.LastName = LastNameTextEdit.Text;
            user.UserName = UserNameTextEdit.Text;
            user.Initial = InitialTextEdit.Text;
            user.Email = EmailTextEdit.Text;
            user.PhoneNumber = PhoneNumberTextEdit.Text;
            //user.CustomerGroups = CurrentUserGroupsListBoxEdit.ItemsSource as ObservableCollection<CustomerGroup>;
            var currentLocationGroups =
                CurrentLocationGroupListBoxEdit.ItemsSource as ObservableCollection<LocationGroup>;
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
        }

        private void AddSelectedLocationGroupToCurrentLocationGroupsOnClick(object sender, RoutedEventArgs e)
        {
            if (AvailableLocationGroupListBoxEdit.SelectedItem is LocationGroup selectedLocationGroup)
            {
                var availableLocationGroups = AvailableLocationGroupListBoxEdit.ItemsSource as ObservableCollection<LocationGroup>;
                var currentLocationGroups = CurrentLocationGroupListBoxEdit.ItemsSource as ObservableCollection<LocationGroup>;
                currentLocationGroups.Add(selectedLocationGroup);
                availableLocationGroups.Remove(selectedLocationGroup);
            }
        }

        private void RemoveSelectedLocationGroupFromCurrentLocationGroupsOnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentLocationGroupListBoxEdit.SelectedItem is LocationGroup selectedLocationGroup)
            {
                var availableLocationGroups = AvailableLocationGroupListBoxEdit.ItemsSource as ObservableCollection<LocationGroup>;
                var currentLocationGroups = CurrentLocationGroupListBoxEdit.ItemsSource as ObservableCollection<LocationGroup>;
                availableLocationGroups.Add(selectedLocationGroup);
                currentLocationGroups.Remove(selectedLocationGroup);
            }
        }
    }
}