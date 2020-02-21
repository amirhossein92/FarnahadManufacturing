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
using DevExpress.Data.Helpers;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Layout.Core.Platform;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.UI.Common;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcVendor : FilterUserControlBase
    {
        private ObservableCollection<Vendor> _vendors;
        private Vendor _activeVendor;
        private ObservableCollection<Address> _addresses;
        private Address _activeAddress;
        private ObservableCollection<ContactInformation> _contactInformations;
        private ContactInformation _activeContactInformation;

        public UcVendor()
        {
            InitializeComponent();

            UserControlTitle = "فروشنده";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            _vendors = new ObservableCollection<Vendor>();
            LoadSearchGridControlData();

            LoadContactTypeComboBox();
            LoadCityComboBox();
            LoadProvinceComboBox();
            LoadAddressTypeComboBox();
            LoadCountryComboBox();
            LoadCarrierComboBoxEdit();
            LoadCarrierServiceComboBoxEdit();
            LoadShippingTermComboBoxEdit();
            LoadTermComboBoxEdit();

            LoadGridControls();
        }

        private void LoadGridControls()
        {
            _addresses = new ObservableCollection<Address>();
            _contactInformations = new ObservableCollection<ContactInformation>();
            AddressesGridControl.ItemsSource = _addresses;
            CurrentContactInformationsGridControl.ItemsSource = _contactInformations;
        }

        private void LoadContactTypeComboBox()
        {
            var contactTypes = new List<FmComboModel<ContactType>>();
            contactTypes.Add(new FmComboModel<ContactType>(ContactType.Email, "پست الکترونیکی"));
            contactTypes.Add(new FmComboModel<ContactType>(ContactType.Fax, "فکس"));
            contactTypes.Add(new FmComboModel<ContactType>(ContactType.Home, "خانه"));
            contactTypes.Add(new FmComboModel<ContactType>(ContactType.Main, "اصلی"));
            contactTypes.Add(new FmComboModel<ContactType>(ContactType.Mobile, "تلفن همراه"));
            contactTypes.Add(new FmComboModel<ContactType>(ContactType.Other, "غیره"));
            contactTypes.Add(new FmComboModel<ContactType>(ContactType.Pager, "پیجر"));
            contactTypes.Add(new FmComboModel<ContactType>(ContactType.Web, "وبسایت"));
            contactTypes.Add(new FmComboModel<ContactType>(ContactType.Work, "محل کار"));
            DefaultContactInformationContactTypeComboBox.ItemsSource = contactTypes;
            CurrentContactInformationContactTypeComboBox.ItemsSource = contactTypes;
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchNameTextEdit.Text, SearchProvinceComboBoxEdit.EditValue as int?,
                SearchCityComboBoxEdit.EditValue as int?);
        }

        private void LoadSearchGridControlData(string searchName = null, int? provinceId = null, int? cityId = null)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var vendorsQueryable = dbContext.Vendors.OrderBy(item => item.Id)
                    .Include(item => item.Addresses)
                    .Include("Addresses.ContactInformations")
                    .Include(item => item.CreatedByUser).AsQueryable();
                if (!string.IsNullOrEmpty(searchName))
                    vendorsQueryable = vendorsQueryable.Where(item => item.Title.Contains(searchName));
                if (provinceId != null)
                    vendorsQueryable = vendorsQueryable.Where(item => item.Addresses.Any(a => a.ProvinceId == provinceId));
                if (cityId != null)
                    vendorsQueryable = vendorsQueryable.Where(item => item.Addresses.Any(a => a.CityId == cityId));
                var totalRecordsCount = vendorsQueryable.Count();
                _vendors = vendorsQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _vendors;
                PaginationUserControl.UpdateRecordsDetail(_vendors.Count, totalRecordsCount);
            }
        }

        private void LoadCityComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var cities = dbContext.Cities.AsNoTracking().Select(item => new { Title = item.Title, Id = item.Id }).ToList();
                SearchCityComboBoxEdit.ItemsSource = cities;
                CurrentCityComboBox.ItemsSource = cities;
                AddressesCityComboBox.ItemsSource = cities;
            }
        }

        private void LoadProvinceComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var provinces = dbContext.Provinces.AsNoTracking().Select(item => new { Title = item.Title, Id = item.Id }).ToList();
                SearchProvinceComboBoxEdit.ItemsSource = provinces;
                CurrentProvinceComboBox.ItemsSource = provinces;
                AddressesProvinceComboBox.ItemsSource = provinces;
            }
        }

        private void LoadTermComboBoxEdit()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var paymentTerms = dbContext.PaymentTerms.AsNoTracking()
                    .Select(item => new { Title = item.Title, Id = item.Id })
                    .ToList();
                DefaultTermComboBoxEdit.ItemsSource = paymentTerms;
            }
        }

        private void LoadShippingTermComboBoxEdit()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var shippingTerms = dbContext.ShippingTerms.AsNoTracking()
                    .Select(item => new { Title = item.Title, Id = item.Id })
                    .ToList();
                DefaultShippingTermComboBoxEdit.ItemsSource = shippingTerms;
            }
        }

        private void LoadCarrierServiceComboBoxEdit()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var carrierServices = dbContext.CarrierServices.AsNoTracking()
                    .Select(item => new { Title = item.Title, Id = item.Id })
                    .ToList();
                DefaultCarrierServiceComboBoxEdit.ItemsSource = carrierServices;
            }
        }

        private void LoadCarrierComboBoxEdit()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var carriers = dbContext.Carriers.AsNoTracking()
                    .Select(item => new { Title = item.Title, Id = item.Id })
                    .ToList();
                DefaultCarrierComboBoxEdit.ItemsSource = carriers;
            }
        }

        private void LoadCountryComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var countries = dbContext.Countries.AsNoTracking()
                    .Select(item => new { Title = item.Title, Id = item.Id })
                    .ToList();
                CurrentCountryComboBox.ItemsSource = countries;
            }
        }

        private void LoadAddressTypeComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var addressTypes = dbContext.AddressTypes.AsNoTracking()
                    .Select(item => new { Title = item.Title, Id = item.Id })
                    .ToList();
                CurrentAddressTypeComboBox.ItemsSource = addressTypes;
                AddressesAddressTypeComboBox.ItemsSource = addressTypes;
            }
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            PaginationUserControl.CurrentPage = 1;
            LoadSearchGridControl();
        }

        protected override void OnAddToolBarItem()
        {
            _activeVendor = new Vendor();
            _addresses = new ObservableCollection<Address>();
            _activeAddress = new Address();
            _contactInformations = new ObservableCollection<ContactInformation>();
            _activeContactInformation = new ContactInformation();
            FillData(_activeVendor);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(ref _activeVendor);
            ReadData(ref _activeAddress);
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                if (_activeVendor.Id > 0)
                {
                    var vendorInDb = dbContext.Vendors.Find(_activeVendor.Id);

                    vendorInDb.Title = _activeVendor.Title;
                    vendorInDb.DefaultPaymentTermId = _activeVendor.DefaultPaymentTermId;
                    vendorInDb.AccountNumber = _activeVendor.AccountNumber;
                    vendorInDb.MinOrderAmount = _activeVendor.MinOrderAmount;
                    vendorInDb.DefaultCarrierId = _activeVendor.DefaultCarrierId;
                    vendorInDb.DefaultCarrierServiceId = _activeVendor.DefaultCarrierServiceId;
                    vendorInDb.DefaultShippingTermId = _activeVendor.DefaultPaymentTermId;
                    vendorInDb.Logo = _activeVendor.Logo;

                    var addressesInDbs = dbContext.Addresses.Where(item => item.CompanyId == _activeVendor.Id).ToList();
                    foreach (var addressInDb in addressesInDbs)
                    {
                        var address = _activeVendor.Addresses.FirstOrDefault(item => item.Id == addressInDb.Id);
                        if (address != null)
                        {
                            addressInDb.Title = address.Title;
                            addressInDb.AddressDetail = address.AddressDetail;
                            addressInDb.IsDefaultAddress = address.IsDefaultAddress;
                            addressInDb.ProvinceId = address.ProvinceId;
                            addressInDb.AddressTypeId = address.AddressTypeId;
                            addressInDb.CityId = address.CityId;
                            addressInDb.CountryId = address.CountryId;
                            addressInDb.IsResidentialAddress = address.IsResidentialAddress;
                            addressInDb.Latitude = address.Latitude;
                            addressInDb.Longitude = address.Longitude;
                            addressInDb.ZipCode = address.ZipCode;

                            var contactInformationsInDbs = dbContext.ContactInformations.Where(item => item.AddressId == addressInDb.Id).ToList();
                            foreach (var contactInformationInDb in contactInformationsInDbs)
                            {
                                var contactInformation = address.ContactInformations.FirstOrDefault(item => item.Id == contactInformationInDb.Id);
                                if (contactInformation != null)
                                {
                                    contactInformationInDb.Title = contactInformation.Title;
                                    contactInformationInDb.IsDefault = contactInformation.IsDefault;
                                    contactInformationInDb.ContactType = contactInformation.ContactType;
                                    contactInformationInDb.Value = contactInformation.Value;
                                }
                                else
                                    dbContext.ContactInformations.Remove(contactInformationInDb);
                            }
                            foreach (var contactInformation in address.ContactInformations.Where(item => item.Id <= 0))
                            {
                                contactInformation.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                                contactInformation.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                                addressInDb.ContactInformations.Add(contactInformation);
                            }
                        }
                        else
                        {
                            dbContext.ContactInformations.RemoveRange(addressInDb.ContactInformations);
                            dbContext.Addresses.Remove(addressInDb);
                        }
                    }

                    foreach (var address in _activeVendor.Addresses.Where(item => item.Id <= 0))
                    {
                        address.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                        address.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                        foreach (var contactInformation in address.ContactInformations)
                        {
                            contactInformation.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                            contactInformation.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                        }
                        vendorInDb.Addresses.Add(address);
                    }
                    dbContext.SaveChanges();
                }
                else
                {
                    _activeVendor.Addresses = _addresses.ToList();
                    foreach (var address in _activeVendor.Addresses)
                    {
                        address.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                        address.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                        foreach (var contactInformation in address.ContactInformations)
                        {
                            contactInformation.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                            contactInformation.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                        }
                    }
                    _activeVendor.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                    _activeVendor.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                    dbContext.Vendors.Add(_activeVendor);

                    dbContext.SaveChanges();
                }
            }

            MessageBoxService.SaveConfirmation(_activeVendor.Title);
            _activeVendor = new Vendor();
            LoadSearchGridControl();
            IsNotEditingAndAdding();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeVendor.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var vendor = dbContext.Vendors
                        .Include(item => item.Addresses)
                        .Include("Addresses.ContactInformations")
                        .First(item => item.Id == _activeVendor.Id);
                    foreach (var address in vendor.Addresses)
                        dbContext.ContactInformations.RemoveRange(address.ContactInformations);
                    dbContext.Addresses.RemoveRange(vendor.Addresses);
                    dbContext.Vendors.Remove(vendor);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeVendor = new Vendor();
                IsNotEditingAndAdding();
            }
        }

        protected override void OnAdding()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeVendor.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateInactiveHeaderTitle(UserControlTitle);
        }

        private void SearchGridControlOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var searchGridControl = (GridControl)sender;
            var rowIndex = searchGridControl.View.GetRowHandleByMouseEventArgs(e);
            if (rowIndex == DataControlBase.InvalidRowHandle)
                return;
            if (searchGridControl.ItemsSource is ObservableCollection<Vendor> vendors)
                EditData(vendors[rowIndex]);
        }

        private void EditData(Vendor vendor)
        {
            _activeVendor = vendor;
            FillData(_activeVendor);
            IsEditing();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            _activeVendor = SearchGridControl.SelectedItem as Vendor;
            EditData(_activeVendor);
        }

        private void FillData(Vendor vendor)
        {
            TitleTextEdit.Text = vendor.Title;
            DefaultAddressUserControl.AddressDetail = vendor.Addresses.FirstOrDefault(item => item.IsDefaultAddress)?.AddressDetail;
            CreatedUserTextEdit.Text = vendor.CreatedByUser?.UserName;
            CreatedDateTextEdit.Text = vendor.CreatedDateTime.ToLongTimeString();
            DefaultContactInformationsGridControl.ItemsSource = vendor.Addresses.FirstOrDefault(item => item.IsDefaultAddress)?.ContactInformations;

            DefaultTermComboBoxEdit.EditValue = vendor.DefaultPaymentTermId;
            DefaultCarrierComboBoxEdit.EditValue = vendor.DefaultCarrierId;
            DefaultCarrierServiceComboBoxEdit.EditValue = vendor.DefaultCarrierServiceId;
            DefaultShippingTermComboBoxEdit.EditValue = vendor.DefaultShippingTermId;
            AccountNumberTextEdit.Text = vendor.AccountNumber;
            MinOrderAmountTextEdit.Text = vendor.MinOrderAmount.ToString();

            _addresses = new ObservableCollection<Address>(vendor.Addresses);
            AddressesGridControl.ItemsSource = _addresses;
            LogoImageEdit.EditValue = vendor.Logo;
        }

        private void ReadData(ref Vendor vendor)
        {
            vendor.Title = TitleTextEdit.Text;

            vendor.DefaultPaymentTermId = DefaultTermComboBoxEdit.EditValue as int?;
            vendor.DefaultCarrierId = DefaultCarrierComboBoxEdit.EditValue as int?;
            vendor.DefaultCarrierServiceId = DefaultCarrierServiceComboBoxEdit.EditValue as int?;
            vendor.DefaultShippingTermId = DefaultShippingTermComboBoxEdit.EditValue as int?;
            vendor.AccountNumber = AccountNumberTextEdit.Text;
            if (double.TryParse(MinOrderAmountTextEdit.Text, out double minOrderAmount))
                vendor.MinOrderAmount = minOrderAmount;

            if (AddressesGridControl.ItemsSource is ObservableCollection<Address> addresses)
                vendor.Addresses = addresses.ToList();
            vendor.Logo = LogoImageEdit.EditValue as byte[];
        }

        private void FillData(Address address)
        {
            CurrentAddressTypeComboBox.EditValue = address.AddressTypeId;
            CurrentAddressTitleTextEdit.EditValue = address.Title;
            CurrentAddressDetailTextEdit.EditValue = address.AddressDetail;
            CurrentCityComboBox.EditValue = address.CityId;
            CurrentProvinceComboBox.EditValue = address.ProvinceId;
            CurrentZipCodeTextEdit.Text = address.ZipCode;
            CurrentCountryComboBox.EditValue = address.CountryId;
            CurrentIsResidentialCheckEdit.EditValue = address.IsResidentialAddress;
            CurrentDefaultAddressCheckEdit.EditValue = address.IsDefaultAddress;

            _contactInformations = new ObservableCollection<ContactInformation>(_contactInformations);
            CurrentContactInformationsGridControl.ItemsSource = _contactInformations;
        }

        private void ReadData(ref Address address)
        {
            address.AddressTypeId = Convert.ToInt32(CurrentAddressTypeComboBox.EditValue);
            address.Title = CurrentAddressTitleTextEdit.Text;
            address.AddressDetail = Convert.ToString(CurrentAddressDetailTextEdit.EditValue);
            address.CityId = CurrentCityComboBox.EditValue as int?;
            address.ProvinceId = CurrentProvinceComboBox.EditValue as int?;
            address.ZipCode = CurrentZipCodeTextEdit.Text;
            address.CountryId = CurrentCountryComboBox.EditValue as int?;
            address.IsResidentialAddress = (bool)CurrentIsResidentialCheckEdit.EditValue;
            address.IsDefaultAddress = (bool)CurrentDefaultAddressCheckEdit.EditValue;
            if (CurrentContactInformationsGridControl.ItemsSource is ObservableCollection<ContactInformation> contactInformations)
                address.ContactInformations = contactInformations.ToList();
        }

        private void AddressesGridControlOnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.OldItem is Address oldAddress)
            {
                ReadData(ref oldAddress);
                oldAddress.ContactInformations = _contactInformations.ToList();
            }
            if (e.NewItem is Address newAddress)
            {
                _activeAddress = newAddress;
                _contactInformations = new ObservableCollection<ContactInformation>(_activeAddress.ContactInformations);
                _activeContactInformation = _contactInformations.FirstOrDefault();
                FillData(_activeAddress);
            }
        }

        private void AddAddressButtonOnClick(object sender, RoutedEventArgs e)
        {
            var newAddress = new Address();
            _addresses.Add(newAddress);
            _activeAddress = newAddress;
            AddressesGridControl.SelectedItem = _activeAddress;
        }

        private void DeleteAddressButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_activeAddress != null)
                _addresses.Remove(_activeAddress);
        }

        private void AddContactInformationOnClick(object sender, RoutedEventArgs e)
        {
            if (_activeAddress != null)
            {
                var newContactInformation = new ContactInformation();
                _contactInformations.Add(newContactInformation);
                _activeContactInformation = newContactInformation;
                CurrentContactInformationsGridControl.SelectedItem = _activeContactInformation;
            }
        }

        private void DeleteContactInformationOnClick(object sender, RoutedEventArgs e)
        {
            if (_activeContactInformation != null)
                _contactInformations.Remove(_activeContactInformation);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }
    }
}
