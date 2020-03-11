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
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcPaymentTerm : FilterUserControlBase
    {
        private ObservableCollection<PaymentTerm> _paymentTerms;
        private PaymentTerm _activePaymentTerm;

        public UcPaymentTerm()
        {
            InitializeComponent();

            UserControlTitle = "شرایط پرداخت";
            ImagePath = "Icons/NavigationBar/Payment_Small.svg";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchGridControl();
            NetDaysRadioButton.IsChecked = true;
            DueDateRadioButton.IsChecked = false;
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchStatusComboBox();
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchStatusComboBoxEdit.EditValue);
        }

        private void LoadSearchStatusComboBox()
        {
            var statuses = new ObservableCollection<FmComboModel<bool>>
            {
                new FmComboModel<bool>(false, "غیر فعال"),
                new FmComboModel<bool>(true, "فعال"),
            };
            SearchStatusComboBoxEdit.ItemsSource = statuses;
        }

        private void LoadSearchGridControlData(string title, object statusComboBox)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var paymentTermsQueryable = dbContext.PaymentTerms.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    paymentTermsQueryable = paymentTermsQueryable.Where(item => item.Title.Contains(title));
                if (statusComboBox is bool isActive)
                    paymentTermsQueryable = paymentTermsQueryable.Where(item => item.IsActive == isActive);
                var totalRecordsCount = paymentTermsQueryable.Count();
                _paymentTerms = paymentTermsQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _paymentTerms;
                PaginationUserControl.UpdateRecordsDetail(_paymentTerms.Count, totalRecordsCount);
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activePaymentTerm = new PaymentTerm();
            FillData(_activePaymentTerm);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activePaymentTerm);
            if (_activePaymentTerm.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activePaymentTerm).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

                IsEditing();
            }
            else
            {
                _activePaymentTerm.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activePaymentTerm.CreatedDateTime = ApplicationSessionService.GetNowDateTime();

                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.PaymentTerms.Add(_activePaymentTerm);
                    dbContext.SaveChanges();
                }

                OnAddToolBarItem();
            }

            MessageBoxService.SaveConfirmation(_activePaymentTerm.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activePaymentTerm.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var paymentTerm = dbContext.PaymentTerms.Find(_activePaymentTerm.Id);
                    dbContext.PaymentTerms.Remove(paymentTerm);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activePaymentTerm = new PaymentTerm();
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
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activePaymentTerm.Title);
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
            if (searchGridControl.ItemsSource is ObservableCollection<PaymentTerm> paymentTerms)
                EditData(paymentTerms[rowIndex]);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var paymentTerm = SearchGridControl.SelectedItem as PaymentTerm;
            EditData(paymentTerm);
        }

        private void EditData(PaymentTerm paymentTerm)
        {
            _activePaymentTerm = paymentTerm;
            FillData(_activePaymentTerm);
            if (_activePaymentTerm.ReadOnly)
                IsNotEditingAndAdding();
            else
                IsEditing();
        }

        private void FillData(PaymentTerm paymentTerm)
        {
            NameTextEdit.Text = paymentTerm.Title;
            DescriptionTextEdit.Text = paymentTerm.Description;
            if (paymentTerm.IsNet)
            {
                NetDaysRadioButton.IsChecked = paymentTerm.IsNet;
                NetNetDaysSpinEdit.EditValue = paymentTerm.NetNetDays;
                NetDiscountPercentageSpinEdit.EditValue = paymentTerm.NetDiscountPercentage;
                NetDiscountDaysSpinEdit.EditValue = paymentTerm.NetDiscountDays;
                DateDrivenDueDateSpinEdit.EditValue = null;
                DateDrivenDiscountDateSpinEdit.EditValue = null;
                DateDrivenDiscountPercentageSpinEdit.EditValue = null;
                DateDrivenNextMonthIfWithinSpinEdit.EditValue = null;
            }
            else
            {
                DueDateRadioButton.IsChecked = true;
                DateDrivenDueDateSpinEdit.EditValue = paymentTerm.DateDrivenDueDate;
                DateDrivenDiscountDateSpinEdit.EditValue = paymentTerm.DateDrivenDiscountDate;
                DateDrivenDiscountPercentageSpinEdit.EditValue = paymentTerm.DateDrivenDiscountPercentage;
                DateDrivenNextMonthIfWithinSpinEdit.EditValue = paymentTerm.DateDrivenNextMonthIfWithin;
                NetNetDaysSpinEdit.EditValue = null;
                NetDiscountPercentageSpinEdit.EditValue = null;
                NetDiscountDaysSpinEdit.EditValue = null;
            }
            IsActiveCheckEdit.EditValue = paymentTerm.IsActive;
        }

        private void ReadData(PaymentTerm paymentTerm)
        {
            paymentTerm.Title = NameTextEdit.Text;
            paymentTerm.Description = DescriptionTextEdit.Text;
            if (NetDaysRadioButton.IsChecked == true)
            {
                paymentTerm.IsNet = true;
                paymentTerm.NetNetDays = (int?)NetNetDaysSpinEdit.EditValue;
                paymentTerm.NetDiscountPercentage = (double?)NetDiscountPercentageSpinEdit.EditValue;
                paymentTerm.NetDiscountDays = (int?)NetDiscountDaysSpinEdit.EditValue;
            }
            else
            {
                paymentTerm.IsNet = false;
                // TODO: VALIDATION (CANNOT BE GREATER THAN 31 and Less than 1)
                paymentTerm.DateDrivenDueDate = (int?)DateDrivenDueDateSpinEdit.EditValue;
                paymentTerm.DateDrivenDiscountDate = (int?)DateDrivenDiscountDateSpinEdit.EditValue;
                paymentTerm.DateDrivenDiscountPercentage = (double?)DateDrivenDiscountPercentageSpinEdit.EditValue;
                paymentTerm.DateDrivenNextMonthIfWithin = (int?)DateDrivenNextMonthIfWithinSpinEdit.EditValue;
            }
            paymentTerm.IsActive = (bool)IsActiveCheckEdit.EditValue;
        }

        private void NetDaysRadioButtonOnChecked(object sender, RoutedEventArgs e)
        {
            NetDiscountDaysSpinEdit.IsEnabled = true;
            NetDiscountPercentageSpinEdit.IsEnabled = true;
            NetNetDaysSpinEdit.IsEnabled = true;
        }

        private void NetDaysRadioButtonOnUnchecked(object sender, RoutedEventArgs e)
        {
            NetDiscountDaysSpinEdit.IsEnabled = false;
            NetDiscountPercentageSpinEdit.IsEnabled = false;
            NetNetDaysSpinEdit.IsEnabled = false;
        }

        private void DueDateRadioButtonOnChecked(object sender, RoutedEventArgs e)
        {
            DateDrivenDueDateSpinEdit.IsEnabled = true;
            DateDrivenDiscountDateSpinEdit.IsEnabled = true;
            DateDrivenDiscountPercentageSpinEdit.IsEnabled = true;
            DateDrivenNextMonthIfWithinSpinEdit.IsEnabled = true;
        }

        private void DueDateRadioButtonOnUnchecked(object sender, RoutedEventArgs e)
        {
            DateDrivenDueDateSpinEdit.IsEnabled = false;
            DateDrivenDiscountDateSpinEdit.IsEnabled = false;
            DateDrivenDiscountPercentageSpinEdit.IsEnabled = false;
            DateDrivenNextMonthIfWithinSpinEdit.IsEnabled = false;
        }
    }
}
