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
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Control.Base.ToolBar.Buttons;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Common;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcMyCompany : UserControlBase
    {
        private MyCompany _myCompany;
        private ObservableCollection<Address> _addresses;
        private Address _activeAddress;
        private ObservableCollection<ContactInformation> _contactInformations;
        private ContactInformation _activeContactInformation;

        public UcMyCompany()
        {
            InitializeComponent();

            SetToolBarItems();
            InitialData();
        }

        protected sealed override void SetToolBarItems()
        {
            var saveButton = new FmSaveBarButtonItem();
            saveButton.ItemClick += SaveButtonOnItemClick;
            ToolBarItems.Add(saveButton.Name, saveButton);
        }

        private void SaveButtonOnItemClick(object sender, ItemClickEventArgs e)
        {
            ReadData(_myCompany);
            ReadData(_activeAddress);
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var myCompanyInDb = dbContext.MyCompanies.Find(_myCompany.Id);

                myCompanyInDb.Title = _myCompany.Title;
                myCompanyInDb.IsTaxExempt = _myCompany.IsTaxExempt;
                myCompanyInDb.DefaultCarrierId = _myCompany.DefaultCarrierId;
                myCompanyInDb.DefaultCarrierServiceId = _myCompany.DefaultCarrierServiceId;
                myCompanyInDb.Logo = _myCompany.Logo;

                var addressesInDbs = dbContext.Addresses.Where(item => item.CompanyId == _myCompany.Id).ToList();
                foreach (var addressInDb in addressesInDbs)
                {
                    var address = _myCompany.Addresses.FirstOrDefault(item => item.Id == addressInDb.Id);
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

                foreach (var address in _myCompany.Addresses.Where(item => item.Id <= 0))
                {
                    address.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                    address.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                    foreach (var contactInformation in address.ContactInformations)
                    {
                        contactInformation.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                        contactInformation.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                    }
                    myCompanyInDb.Addresses.Add(address);
                }
                dbContext.SaveChanges();
            }

            MessageBoxService.SaveConfirmation(_myCompany.Title);
            LoadMyCompany();
        }

        protected sealed override void InitialData()
        {
            // TODO: Connect related combo boxes => city,province and country
            LoadContactTypeComboBox();
            LoadAddressTypeComboBox();
            LoadCarrierComboBox();
            LoadCarrierServiceComboBox();
            LoadCountryComboBox();
            LoadProvinceComboBox();
            LoadCityComboBox();

            LoadMyCompany();
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

        private void LoadCarrierComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var carriers = dbContext.Carriers
                    .Select(item => new FmComboModel<int> { Value = item.Id, Title = item.Title })
                    .ToList();
                DefaultCarrierComboBoxEdit.ItemsSource = carriers;
            }
        }

        private void LoadCarrierServiceComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var carrierServices = dbContext.CarrierServices.AsNoTracking()
                    .Select(item => new FmComboModel<int> { Title = item.Title, Value = item.Id })
                    .ToList();
                DefaultCarrierServiceComboBoxEdit.ItemsSource = carrierServices;
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

        private void LoadCityComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var cities = dbContext.Cities.AsNoTracking().Select(item => new { Title = item.Title, Id = item.Id }).ToList();
                CurrentCityComboBox.ItemsSource = cities;
                AddressesCityComboBox.ItemsSource = cities;
            }
        }

        private void LoadProvinceComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var provinces = dbContext.Provinces.AsNoTracking().Select(item => new { Title = item.Title, Id = item.Id }).ToList();
                CurrentProvinceComboBox.ItemsSource = provinces;
                AddressesProvinceComboBox.ItemsSource = provinces;
            }
        }

        private void LoadMyCompany()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                // TODO: ADD ONE MY COMPANY IN SEED
                _myCompany = dbContext.MyCompanies
                    .Include(item => item.Addresses)
                    .Include("Addresses.ContactInformations")
                    .Include(item => item.CreatedByUser).First();
                FillData(_myCompany);
                MainLayoutGroup.IsEnabled = true;
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

        private void FillData(MyCompany myCompany)
        {
            TitleTextEdit.Text = myCompany.Title;

            DefaultAddressUserControl.AddressDetail = myCompany.Addresses.FirstOrDefault(item => item.IsDefaultAddress)?.AddressDetail;
            CreatedUserTextEdit.Text = myCompany.CreatedByUser?.UserName;
            CreatedDateTextEdit.Text = myCompany.CreatedDateTime.ToLongTimeString();
            DefaultContactInformationsGridControl.ItemsSource = myCompany.Addresses.FirstOrDefault(item => item.IsDefaultAddress)?.ContactInformations;

            DefaultCarrierComboBoxEdit.EditValue = myCompany.DefaultCarrierId;
            DefaultCarrierServiceComboBoxEdit.EditValue = myCompany.DefaultCarrierId;
            IsTaxExemptCheckEdit.EditValue = myCompany.IsTaxExempt;

            LogoImageEdit.EditValue = myCompany.Logo;

            _addresses = new ObservableCollection<Address>(myCompany.Addresses);
            AddressesGridControl.ItemsSource = _addresses;
        }

        private void ReadData(MyCompany myCompany)
        {
            myCompany.Title = TitleTextEdit.Text;

            myCompany.DefaultCarrierId = DefaultCarrierComboBoxEdit.EditValue as int?;
            myCompany.DefaultCarrierServiceId = DefaultCarrierServiceComboBoxEdit.EditValue as int?;
            myCompany.IsTaxExempt = Convert.ToBoolean(IsTaxExemptCheckEdit.EditValue);
            if (AddressesGridControl.ItemsSource is ObservableCollection<Address> addresses)
                myCompany.Addresses = addresses.ToList();
            myCompany.Logo = LogoImageEdit.EditValue as byte[];
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

        private void ReadData(Address address)
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
                ReadData(oldAddress);
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
    }
}
