using System;
using System.Collections.Generic;
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
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.UI.Common;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcCarrierService : DialogUserControlBase
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
        }

        private void FillData(CarrierService carrierService)
        {
            TitleTextEdit.Text = carrierService.Title;
            CodeTextEdit.Text = carrierService.Code;
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
            CloseWindow();
        }

        private void ReadData(CarrierService carrierService)
        {
            carrierService.Title = TitleTextEdit.Text;
            carrierService.Code = CodeTextEdit.Text;
        }

        private void CancelButtonOnClick(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            WindowService.CloseUserControlDialogWindow(this);
        }
    }
}
