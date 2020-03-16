using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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
    public partial class UcTracking : FilterUserControlPage
    {
        private ObservableCollection<Tracking> _trackings;
        private Tracking _activeTracking;

        public UcTracking()
        {
            InitializeComponent();

            UserControlTitle = "ردیابی";
            ImagePath = "Icons/NavigationBar/Tracking_Small.svg";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchGridControl();
            LoadTrackingValueTypes();
        }

        private void LoadTrackingValueTypes()
        {
            var trackingValueTypes = new ObservableCollection<FmComboModel<TrackingValueType>>
            {
                new FmComboModel<TrackingValueType>(TrackingValueType.Checkbox, "چک باکس"),
                new FmComboModel<TrackingValueType>(TrackingValueType.Text, "متن"),
                new FmComboModel<TrackingValueType>(TrackingValueType.Count, "شمارش"),
                new FmComboModel<TrackingValueType>(TrackingValueType.Date, "تاریخ"),
                new FmComboModel<TrackingValueType>(TrackingValueType.Quantity, "تعداد"),
                new FmComboModel<TrackingValueType>(TrackingValueType.ExpirationDate, "تاریخ انقضا"),
                new FmComboModel<TrackingValueType>(TrackingValueType.SerialNumber, "شماره سریال"),
            };
            TrackingTypeComboBoxEdit.ItemsSource = trackingValueTypes;
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text);
        }

        private void LoadSearchGridControlData(string title)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var trackingsQueryable = dbContext.Trackings.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    trackingsQueryable = trackingsQueryable.Where(item => item.Title.Contains(title));
                var totalRecordsCount = trackingsQueryable.Count();
                _trackings = trackingsQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _trackings;
                PaginationUserControl.UpdateRecordsDetail(_trackings.Count, totalRecordsCount);
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activeTracking = new Tracking();
            FillData(_activeTracking);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeTracking);
            if (_activeTracking.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeTracking).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    IsEditing();
                }
            }
            else
            {
                _activeTracking.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeTracking.CreatedDateTime = ApplicationSessionService.GetNowDateTime();

                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Trackings.Add(_activeTracking);
                    dbContext.SaveChanges();
                }

                OnAddToolBarItem();
            }

            MessageBoxService.SaveConfirmation(_activeTracking.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeTracking.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var tracking = dbContext.Trackings.Find(_activeTracking.Id);
                    dbContext.Trackings.Remove(tracking);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeTracking = new Tracking();
                IsNotEditingAndAdding();
            }
        }

        protected override void OnAdding()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
            NameTextEdit.Focus();
        }

        protected override void OnEditing()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeTracking.Title);
            NameTextEdit.Focus();
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
            if (searchGridControl.ItemsSource is ObservableCollection<Tracking> trackings)
                EditData(trackings[rowIndex]);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var tracking = SearchGridControl.SelectedItem as Tracking;
            EditData(tracking);
        }

        private void EditData(Tracking tracking)
        {
            _activeTracking = tracking;
            FillData(_activeTracking);
            IsEditing();
        }

        private void FillData(Tracking tracking)
        {
            NameTextEdit.Text = tracking.Title;
            AbbreviationTextEdit.Text = tracking.Abbreviation;
            TrackingTypeComboBoxEdit.EditValue = tracking.TrackingValueType;
            DescriptionTextEdit.Text = tracking.Description;
        }

        private void ReadData(Tracking tracking)
        {
            tracking.Title = NameTextEdit.Text;
            tracking.Abbreviation = AbbreviationTextEdit.Text;
            tracking.TrackingValueType = (TrackingValueType)TrackingTypeComboBoxEdit.EditValue;
            tracking.Description = DescriptionTextEdit.Text;
        }
    }
}
