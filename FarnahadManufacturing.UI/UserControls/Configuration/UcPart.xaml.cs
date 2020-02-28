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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Common;

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
                // TODO: Change UOM Type to enum;
                var distanceUoms = dbContext.Uoms.AsNoTracking()
                    .Where(item => item.UomTypeId == 3).ToList();
                DistanceUomComboBoxEdit.ItemsSource = distanceUoms;
            }
        }

        private void LoadWeightUomComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var weightUoms = dbContext.Uoms.AsNoTracking()
                    .Where(item => item.UomTypeId == 2).ToList();
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
            // TODO: Complete enum list
            var trackingValueTypes = new List<FmComboModel<TrackingValueType>>
            {
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
            // TODO: complete the list
            var partTypes = new List<FmComboModel<PartType>>
            {
                new FmComboModel<PartType>(PartType.CapitalEquipment, "CapitalEquipment"),
                new FmComboModel<PartType>(PartType.InternalUse, "InternalUse"),
                new FmComboModel<PartType>(PartType.Inventory, "Inventory"),
                new FmComboModel<PartType>(PartType.Labor, "Labor"),
                new FmComboModel<PartType>(PartType.Service, "Service"),
            };
            PartTypeComboBoxEdit.ItemsSource = partTypes;
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
                var partsQueryable = dbContext.Parts.OrderBy(item => item.Id).AsQueryable();
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
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activePart).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            else
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    _activePart.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                    _activePart.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                    dbContext.Parts.Add(_activePart);
                    dbContext.SaveChanges();
                }
            }
        }

        protected override void OnDeleteToolBarItem()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var partInDb = dbContext.Parts.FirstOrDefault(item => item.Id == _activePart.Id);
                dbContext.Parts.Remove(partInDb);
                dbContext.SaveChanges();
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
            LengthSpinEdit.EditValue = part.Width;
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
                    .Where(item => item.Id == partId).ToList();
                var trackingIds = dbContext.Trackings.Select(item => item.Id).ToList();
                foreach (var trackingId in trackingIds)
                {
                    if (trackingParts.All(item => item.TrackingId != trackingId))
                    {
                        var tracking = dbContext.Trackings.First(item => item.Id == trackingId);
                        trackingParts.Add(new TrackingPart
                        {
                            Tracking = tracking,
                            PartId = partId,
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
                var partDefaultLocations = dbContext.PartDefaultLocations.Where(item => item.Id == partId).ToList();
                var locationGroupIds = dbContext.LocationGroups.Select(item => item.Id).ToList();
                foreach (var locationGroupId in locationGroupIds)
                {
                    if (partDefaultLocations.All(item => item.LocationGroupId != locationGroupId))
                    {
                        partDefaultLocations.Add(new PartDefaultLocation
                        {
                            LocationGroupId = locationGroupId,
                            PartId = partId,
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
                var products = dbContext.Products
                    .Where(item => item.Id == partId && item.IsActive == !showInActiveProducts)
                    .ToList();
                ProductsGridControl.ItemsSource = products;
            }
        }

        private void ReadData(Part part)
        {
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
            // CREATE COST
            //CostSpinEdit.EditValue = part.PartCosts.OrderByDescending(item => item.CreatedDateTime).First()?.Cost;
            part.AlertNote = AlertNoteTextEdit.Text;
            part.Width = (double?)LengthSpinEdit.EditValue;
            part.Width = (double?)WidthSpinEdit.EditValue;
            part.Height = (double?)HeightSpinEdit.EditValue;
            part.DistanceUomId = (int)DistanceUomComboBoxEdit.EditValue;
            part.Weight = (double?)WeightSpinEdit.EditValue;
            part.WeightUomId = (int)WeightUomComboBoxEdit.EditValue;
            // Tracking

            part.PartAbcCode = (PartAbcCode)PartAbcCodeComboBoxEdit.EditValue;
            // UPDATE REORDER INFORMATION
            //ReorderInformationGridControl.ItemsSource = new ObservableCollection<PartReorderInformation>(part.PartReorderInformations);
            // UPDATE DEFAULT LOCATIONS
            DefaultLocationGridControl.ItemsSource = new ObservableCollection<PartDefaultLocation>(part.PartDefaultLocations);

            // Vendors
        }

        private void ProductsGridControlOnCustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                if (e.Column.FieldName == "Price")
                {

                }
            }
        }

        private void ShowInActiveProductCheckEditOnEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            LoadProducts(_activePart.Id, (bool)e.NewValue);
        }
    }
}
