using System.Linq;
using System.Windows;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcPartReorderInformation : DialogUserControlBase
    {
        private PartReorderInformation _partReorderInformation;

        public UcPartReorderInformation()
        {
            InitializeComponent();
        }

        public UcPartReorderInformation(PartReorderInformation partReorderInformation) : this()
        {
            LoadLocationGroups();
            _partReorderInformation = partReorderInformation;
            FillData(_partReorderInformation);

            if (partReorderInformation.Id > 0)
                UserControlTitle = "ویرایش سفارش دوباره";
            else
                UserControlTitle = "اضافه کردن سفارش دوباره";
        }

        private void LoadLocationGroups()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var locationGroups = dbContext.LocationGroups
                    .Select(item => new FmComboModel<int>
                    {
                        Value = item.Id,
                        Title = item.Title,
                    }).ToList();
                LocationGroupComboBoxEdit.ItemsSource = locationGroups;
            }
        }

        private void SaveButtonOnClick(object sender, RoutedEventArgs e)
        {
            ReadData(_partReorderInformation);
            ApplicationDataStore.SendData("IsAddedOrChanged", true);
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void CancelButtonOnClick(object sender, RoutedEventArgs e)
        {
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void FillData(PartReorderInformation partReorderInformation)
        {
            LocationGroupComboBoxEdit.EditValue = partReorderInformation.LocationGroupId;
            ReorderPointSpinEdit.EditValue = partReorderInformation.ReorderPoint;
            OrderUpToLevelSpinEdit.EditValue = partReorderInformation.OrderUpToLevel;
        }

        private void ReadData(PartReorderInformation partReorderInformation)
        {
            partReorderInformation.LocationGroupId = (int)LocationGroupComboBoxEdit.EditValue;
            partReorderInformation.ReorderPoint = (double)ReorderPointSpinEdit.EditValue;
            partReorderInformation.OrderUpToLevel = (double)OrderUpToLevelSpinEdit.EditValue;
        }
    }
}
