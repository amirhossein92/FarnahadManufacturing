using System;
using System.Collections;
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
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.GridControl;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Common;
using WindowService = FarnahadManufacturing.Control.Common.WindowService;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcPart : FilterUserControlBase
    {
        private ObservableCollection<Part> _parts;
        private Part _activePart;

        public UcPart()
        {
            InitializeComponent();

            UserControlTitle = "کالا";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchGridControl();

            LoadUomComboBox();
            LoadDistanceUomComboBox();
            LoadWeightUomComboBox();
            LoadPartAbcCodeComboBox();
            LoadTrackingValueTypeComboBox();
            LoadLocationGroupComboBox();
            LoadLocationComboBox();
            LoadPartTypes();
        }

        private void LoadUomComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var uoms = dbContext.Uoms.AsNoTracking().ToList();
                UomComboBoxEdit.ItemsSource = uoms;
            }
        }

        private void LoadDistanceUomComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var distanceUoms = dbContext.Uoms.AsNoTracking()
                    .Where(item => item.UomType == UomType.Length).ToList();
                DistanceUomComboBoxEdit.ItemsSource = distanceUoms;
            }
        }

        private void LoadWeightUomComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var weightUoms = dbContext.Uoms.AsNoTracking()
                    .Where(item => item.UomType == UomType.Weight).ToList();
                WeightUomComboBoxEdit.ItemsSource = weightUoms;
            }
        }

        private void LoadPartAbcCodeComboBox()
        {
            var partAbcCodes = new List<FmComboModel<PartAbcCode>>
            {
                new FmComboModel<PartAbcCode>(PartAbcCode.A, "بیش از 75 درصد"),
                new FmComboModel<PartAbcCode>(PartAbcCode.B, "کمتر از 25 درصد"),
                new FmComboModel<PartAbcCode>(PartAbcCode.C, "کمتر از 5 درصد"),
            };
            PartAbcCodeComboBoxEdit.ItemsSource = partAbcCodes;
        }

        private void LoadTrackingValueTypeComboBox()
        {
            var trackingValueTypes = new List<FmComboModel<TrackingValueType>>
            {
                new FmComboModel<TrackingValueType>(TrackingValueType.SerialNumber, "شماره سریال"),
                new FmComboModel<TrackingValueType>(TrackingValueType.ExpirationDate, "تاریخ انقضا"),
                new FmComboModel<TrackingValueType>(TrackingValueType.Date, "تاریخ"),
                new FmComboModel<TrackingValueType>(TrackingValueType.Checkbox, "چک باکس"),
                new FmComboModel<TrackingValueType>(TrackingValueType.Text, "متن"),
                new FmComboModel<TrackingValueType>(TrackingValueType.Count, "شمارش"),
                new FmComboModel<TrackingValueType>(TrackingValueType.Quantity, "تعداد"),
            };
            TrackingTypeComboBox.ItemsSource = trackingValueTypes;
        }

        private void LoadLocationGroupComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var locationGroups = dbContext.LocationGroups.AsNoTracking()
                    .Select(item => new FmComboModel<int> { Title = item.Title, Value = item.Id })
                    .ToList();
                LocationGroupComboBox.ItemsSource = locationGroups;
                DefaultLocationGroupComboBox.ItemsSource = locationGroups;
            }
        }

        private void LoadLocationComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var locations = dbContext.Locations.AsNoTracking()
                    .Select(item => new FmComboModel<int> { Title = item.Title, Value = item.Id })
                    .ToList();
                DefaultLocationComboBox.ItemsSource = locations;
            }
        }

        private void LoadPartTypes()
        {
            var partTypes = new List<FmComboModel<PartType>>
            {
                new FmComboModel<PartType>(PartType.Inventory, "کالای قابل ذخیره در انبار"),
                new FmComboModel<PartType>(PartType.NonInventory, "کالای غیر قابل ذخیره در انبار"),
                new FmComboModel<PartType>(PartType.CapitalEquipment, "تجهیزات سرمایه ای"),
                new FmComboModel<PartType>(PartType.InternalUse, "استفاده داخلی"),
                new FmComboModel<PartType>(PartType.Labor, "فعالیت"),
                new FmComboModel<PartType>(PartType.Service, "خدمت"),
                new FmComboModel<PartType>(PartType.Overhead, "هزینه سربار"),
                new FmComboModel<PartType>(PartType.Shipping, "ترابری"),
                new FmComboModel<PartType>(PartType.Misc, "متفرقه"),
                //new FmComboModel<PartType>(PartType.Tax, "مالیات"),
            };
            PartTypeComboBoxEdit.ItemsSource = partTypes;
            SearchPartTypeComboBoxEdit.ItemsSource = partTypes;
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchPartNumberTextEdit.Text, SearchDescriptionTextEdit.Text,
                SearchPartTypeComboBoxEdit.EditValue);
        }

        private void LoadSearchGridControlData(string partNumber, string description, object partType)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var partsQueryable = dbContext.Parts.OrderBy(item => item.Id)
                    .Include(item => item.PartCosts)
                    .Include(item => item.PartReorderInformations)
                    .Include(item => item.LastChangedByUser)
                    .AsQueryable();
                if (!string.IsNullOrEmpty(partNumber))
                    partsQueryable = partsQueryable.Where(item => item.Number.Contains(partNumber));
                if (!string.IsNullOrEmpty(description))
                    partsQueryable = partsQueryable.Where(item => item.Description.Contains(description));
                if (partType is PartType partTypeValue)
                    partsQueryable = partsQueryable.Where(item => item.PartType == partTypeValue);
                _parts = partsQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _parts;
                PaginationUserControl.UpdateRecordsDetail(_parts.Count, partsQueryable.Count());
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activePart = new Part();
            FillData(_activePart);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activePart);
            if (_activePart.Id > 0)
            {
                var activeUserId = ApplicationSessionService.GetActiveUserId();
                var creationDate = ApplicationSessionService.GetNowDateTime();

                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var partInDb = dbContext.Parts.First(item => item.Id == _activePart.Id);
                    var newCost = _activePart.PartCosts.FirstOrDefault(item => item.CreatedByUserId == 0);
                    if (newCost != null)
                    {
                        newCost.CreatedByUserId = activeUserId;
                        newCost.CreatedDateTime = creationDate;
                        newCost.PartId = partInDb.Id;
                        partInDb.PartCosts.Add(newCost);
                    }

                    foreach (var defaultLocation in _activePart.PartDefaultLocations)
                    {
                        if (defaultLocation.PartId == 0)
                            defaultLocation.PartId = _activePart.Id;
                        else
                            dbContext.Entry(defaultLocation).State = EntityState.Modified;
                    }
                    partInDb.PartDefaultLocations = _activePart.PartDefaultLocations;

                    foreach (var trackingPart in _activePart.TrackingParts)
                    {
                        if (trackingPart.PartId == 0)
                        {
                            trackingPart.PartId = _activePart.Id;
                            trackingPart.CreatedByUserId = activeUserId;
                            trackingPart.CreatedDateTime = creationDate;
                        }
                        else
                            dbContext.Entry(trackingPart).State = EntityState.Modified;
                    }
                    partInDb.TrackingParts = _activePart.TrackingParts;

                    foreach (var partReorderInformation in _activePart.PartReorderInformations)
                    {
                        if (partReorderInformation.PartId == 0)
                        {
                            partReorderInformation.PartId = _activePart.Id;
                            partReorderInformation.CreatedByUserId = activeUserId;
                            partReorderInformation.CreatedDateTime = creationDate;
                        }
                        else if (partInDb.PartReorderInformations.Any(item => item.Id == partReorderInformation.Id))
                            dbContext.Entry(partReorderInformation).State = EntityState.Deleted;
                        else
                            dbContext.Entry(partReorderInformation).State = EntityState.Modified;
                    }
                    partInDb.PartReorderInformations = _activePart.PartReorderInformations;

                    //_activePart.Products;

                    partInDb.Title = _activePart.Title;
                    partInDb.Number = _activePart.Number;
                    partInDb.UomId = _activePart.UomId;
                    partInDb.PartType = _activePart.PartType;
                    partInDb.Description = _activePart.Description;
                    partInDb.Details = _activePart.Details;
                    partInDb.IsActive = _activePart.IsActive;
                    partInDb.PickInPartUomOnly = _activePart.PickInPartUomOnly;
                    partInDb.Picture = _activePart.Picture;

                    partInDb.RevisionNumber = _activePart.RevisionNumber;
                    partInDb.Upc = _activePart.Upc;
                    partInDb.AlertNote = _activePart.AlertNote;
                    partInDb.Length = _activePart.Length;
                    partInDb.Width = _activePart.Width;
                    partInDb.Height = _activePart.Height;
                    partInDb.DistanceUomId = _activePart.DistanceUomId;
                    partInDb.Weight = _activePart.Weight;
                    partInDb.WeightUomId = _activePart.WeightUomId;

                    partInDb.PartAbcCode = _activePart.PartAbcCode;

                    partInDb.LastChangedByUserId = activeUserId;
                    partInDb.LastChangedDateTime = creationDate;

                    dbContext.SaveChanges();
                }
            }
            else
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var activeUserId = ApplicationSessionService.GetActiveUserId();
                    var creationDate = ApplicationSessionService.GetNowDateTime();
                    _activePart.CreatedByUserId = activeUserId;
                    _activePart.LastChangedByUserId = activeUserId;
                    _activePart.CreatedDateTime = creationDate;
                    _activePart.LastChangedDateTime = creationDate;
                    foreach (var trackingPart in _activePart.TrackingParts)
                    {
                        dbContext.Trackings.Attach(trackingPart.Tracking);
                        trackingPart.CreatedByUserId = activeUserId;
                        trackingPart.CreatedDateTime = creationDate;
                    }

                    foreach (var partCost in _activePart.PartCosts)
                    {
                        partCost.CreatedByUserId = activeUserId;
                        partCost.CreatedDateTime = creationDate;
                    }

                    foreach (var reorderInformation in _activePart.PartReorderInformations)
                    {
                        reorderInformation.CreatedByUserId = activeUserId;
                        reorderInformation.CreatedDateTime = creationDate;
                    }

                    dbContext.Parts.Add(_activePart);
                    dbContext.SaveChanges();
                }
            }

            MessageBoxService.SaveConfirmation(_activePart.Title);
            LoadSearchGridControl();
            IsEditing();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activePart.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var partInDb = dbContext.Parts.FirstOrDefault(item => item.Id == _activePart.Id);
                    dbContext.Parts.Remove(partInDb);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activePart = new Part();
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
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activePart.Title);
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
            if (searchGridControl.ItemsSource is ObservableCollection<Part> parts)
                EditData(parts[rowIndex]);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var part = SearchGridControl.SelectedItem as Part;
            EditData(part);
        }

        private void EditData(Part part)
        {
            _activePart = part;
            FillData(_activePart);
            IsEditing();
        }

        private void FillData(Part part)
        {
            PartTitleTextEdit.Text = part.Title;
            PartNumberTextEdit.Text = part.Number;
            UomComboBoxEdit.EditValue = part.UomId;
            PartTypeComboBoxEdit.EditValue = part.PartType;
            DescriptionTextEdit.Text = part.Description;
            IsActiveCheckEdit.EditValue = part.IsActive;
            PickInPartUomOnlyCheckEdit.EditValue = part.PickInPartUomOnly;
            DetailsTextEdit.Text = part.Details;
            PictureImageEdit.EditValue = part.Picture;

            RevisionNumberTextEdit.Text = part.RevisionNumber;
            UpcTextEdit.Text = part.Upc;
            CostSpinEdit.EditValue = part.PartCosts.OrderByDescending(item => item.CreatedDateTime).FirstOrDefault()?.Cost;
            AlertNoteTextEdit.Text = part.AlertNote;
            CreatedDateLabel.Text = part.CreatedDateTime.ToLongTimeString();
            LastChangedDateLabel.Text = part.LastChangedDateTime.ToLongTimeString();
            LastUserLabel.Text = part.LastChangedByUser?.UserName;
            LengthSpinEdit.EditValue = part.Length;
            WidthSpinEdit.EditValue = part.Width;
            HeightSpinEdit.EditValue = part.Height;
            DistanceUomComboBoxEdit.EditValue = part.DistanceUomId;
            WeightSpinEdit.EditValue = part.Weight;
            WeightUomComboBoxEdit.EditValue = part.WeightUomId;
            LoadTrackings(part.Id);
            LoadProducts(part.Id);

            PartAbcCodeComboBoxEdit.EditValue = part.PartAbcCode;
            UomLabel.Text = part.Uom?.Abbreviation;
            // Inventories
            ReorderInformationGridControl.ItemsSource = new ObservableCollection<PartReorderInformation>(part.PartReorderInformations);
            LoadDefaultLocations(part.Id);

            // BOMs

            // Vendors
        }

        private void LoadTrackings(int partId)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var trackingParts = dbContext.TrackingParts
                    .Include(item => item.Tracking)
                    .Where(item => item.PartId == partId).ToList();
                var trackingIds = dbContext.Trackings.Select(item => item.Id).ToList();
                foreach (var trackingId in trackingIds)
                {
                    if (trackingParts.All(item => item.TrackingId != trackingId))
                    {
                        var tracking = dbContext.Trackings.First(item => item.Id == trackingId);
                        trackingParts.Add(new TrackingPart
                        {
                            Tracking = tracking
                        });
                    }
                }
                TrackingGridControl.ItemsSource = new ObservableCollection<TrackingPart>(trackingParts);
            }
        }

        private void LoadDefaultLocations(int partId)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var partDefaultLocations = dbContext.PartDefaultLocations.Where(item => item.PartId == partId).ToList();
                var locationGroupIds = dbContext.LocationGroups.Select(item => item.Id).ToList();
                foreach (var locationGroupId in locationGroupIds)
                {
                    if (partDefaultLocations.All(item => item.LocationGroupId != locationGroupId))
                    {
                        partDefaultLocations.Add(new PartDefaultLocation
                        {
                            LocationGroupId = locationGroupId
                        });
                    }
                }
                DefaultLocationGridControl.ItemsSource = new ObservableCollection<PartDefaultLocation>(partDefaultLocations);
            }
        }

        private void LoadProducts(int partId, bool showInActiveProducts = false)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var productsQueryable = dbContext.Products
                    .Where(item => item.PartId == partId).AsQueryable();
                if (!showInActiveProducts)
                    productsQueryable = productsQueryable.Where(item => item.IsActive == true);
                var products = productsQueryable.ToList();
                ProductsGridControl.ItemsSource = products;
            }
        }

        private void ReadData(Part part)
        {
            part.Title = PartTitleTextEdit.Text;
            part.Number = PartNumberTextEdit.Text;
            part.UomId = Convert.ToInt32(UomComboBoxEdit.EditValue);
            part.PartType = (PartType)PartTypeComboBoxEdit.EditValue;
            part.Description = DescriptionTextEdit.Text;
            part.IsActive = (bool)IsActiveCheckEdit.EditValue;
            part.PickInPartUomOnly = (bool)PickInPartUomOnlyCheckEdit.EditValue;
            part.Details = DetailsTextEdit.Text;
            part.Picture = (byte[])PictureImageEdit.EditValue;

            part.RevisionNumber = RevisionNumberTextEdit.Text;
            part.Upc = UpcTextEdit.Text;
            var lastCost = part.PartCosts.OrderByDescending(item => item.CreatedDateTime).FirstOrDefault()?.Cost;
            if (lastCost == null || (lastCost != null && lastCost != Convert.ToDouble(CostSpinEdit.EditValue)))
                part.PartCosts.Add(new PartCost { Cost = Convert.ToDouble(CostSpinEdit.EditValue) });
            part.AlertNote = AlertNoteTextEdit.Text;
            part.Length = (double?)LengthSpinEdit.EditValue;
            part.Width = (double?)WidthSpinEdit.EditValue;
            part.Height = (double?)HeightSpinEdit.EditValue;
            part.DistanceUomId = (int)DistanceUomComboBoxEdit.EditValue;
            part.Weight = (double?)WeightSpinEdit.EditValue;
            part.WeightUomId = (int)WeightUomComboBoxEdit.EditValue;
            if (TrackingGridControl.ItemsSource is ObservableCollection<TrackingPart> trackingParts)
                part.TrackingParts = trackingParts.ToList();
            part.PartAbcCode = (PartAbcCode)PartAbcCodeComboBoxEdit.EditValue;
            // UPDATE REORDER INFORMATION
            if (ReorderInformationGridControl.ItemsSource is ObservableCollection<PartReorderInformation> partReorderInformations)
                part.PartReorderInformations = partReorderInformations.ToList();
            // UPDATE DEFAULT LOCATIONS
            if (DefaultLocationGridControl.ItemsSource is ObservableCollection<PartDefaultLocation> partDefaultLocations)
                part.PartDefaultLocations = partDefaultLocations.ToList();

            // UPDATE Vendors
        }

        private void ProductsGridControlOnCustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            var product = ((IList)((FmEditModeGridControl)sender).ItemsSource)[e.ListSourceRowIndex] as Product;
            if (e.IsGetData)
            {
                if (e.Column.FieldName == "Price")
                {
                    e.Value = GetLastProductPriceByProductId(product.Id);
                }
            }
        }

        private double? GetLastProductPriceByProductId(int productId)
        {
            double? result = null;
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var lastProductPrice = dbContext.ProductPrices.Where(item => item.ProductId == productId)
                    .OrderByDescending(item => item.CreatedDateTime).FirstOrDefault();
                if (lastProductPrice != null)
                    result = lastProductPrice.Price;
            }
            return result;
        }

        private void ShowInActiveProductCheckEditOnEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            LoadProducts(_activePart.Id, (bool)e.NewValue);
        }

        private void AddEditDeleteListUserControlOnClickOnAddItem(object sender, RoutedEventArgs e)
        {
            var partReorderInformation = new PartReorderInformation();
            WindowService.OpenUserControlDialog(new UcPartReorderInformation(partReorderInformation));
            var dataIsAdded = ApplicationDataStore.GetData<bool>("IsAddedOrChanged");
            if (dataIsAdded)
            {
                _activePart.PartReorderInformations.Add(partReorderInformation);
                ReorderInformationGridControl.ItemsSource =
                    new ObservableCollection<PartReorderInformation>(_activePart.PartReorderInformations);
            }
        }

        private void AddEditDeleteListUserControlOnClickOnEditItem(object sender, RoutedEventArgs e)
        {
            if (ReorderInformationGridControl.SelectedItem is PartReorderInformation partReorderInformation)
            {
                WindowService.OpenUserControlDialog(new UcPartReorderInformation(partReorderInformation));
                var dataIsChanged = ApplicationDataStore.GetData<bool>("IsAddedOrChanged");
                if (dataIsChanged)
                {
                    ReorderInformationGridControl.ItemsSource =
                        new ObservableCollection<PartReorderInformation>(_activePart.PartReorderInformations);
                }
            }
        }

        private void AddEditDeleteListUserControlOnClickOnDeleteItem(object sender, RoutedEventArgs e)
        {
            if (ReorderInformationGridControl.SelectedItem is PartReorderInformation partReorderInformation)
            {
                if (MessageBoxService.AskForDelete() == true)
                    _activePart.PartReorderInformations.Remove(partReorderInformation);
            }
        }
    }
}
