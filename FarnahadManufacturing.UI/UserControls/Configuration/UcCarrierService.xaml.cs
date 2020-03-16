using System.Windows;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcCarrierService : DialogUserControl
    {
        private CarrierService _activeCarrierService;

        public UcCarrierService()
        {
            InitializeComponent();
            UserControlTitle = "خدمت پیک";
        }

        public UcCarrierService(CarrierService carrierService) : this()
        {
            _activeCarrierService = carrierService;
            if (_activeCarrierService.Id > 0)
            {
                FillData(_activeCarrierService);
            }

            TitleTextEdit.Focus();
        }

        private void SaveButtonOnClick(object sender, RoutedEventArgs e)
        {
            ReadData(_activeCarrierService);
            if (_activeCarrierService.Id <= 0)
            {
                _activeCarrierService.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeCarrierService.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
            }
            ApplicationDataStore.SendData("CarrierService", _activeCarrierService);
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void CancelButtonOnClick(object sender, RoutedEventArgs e)
        {
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void FillData(CarrierService carrierService)
        {
            TitleTextEdit.Text = carrierService.Title;
            CodeTextEdit.Text = carrierService.Code;
        }

        private void ReadData(CarrierService carrierService)
        {
            carrierService.Title = TitleTextEdit.Text;
            carrierService.Code = CodeTextEdit.Text;
        }
    }
}
