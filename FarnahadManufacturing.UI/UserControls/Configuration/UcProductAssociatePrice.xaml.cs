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
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcProductAssociatePrice : DialogUserControlBase
    {
        private ProductAssociatePrice _productAssociatePrice;

        private UcProductAssociatePrice()
        {
            InitializeComponent();
        }

        public UcProductAssociatePrice(ProductAssociatePrice productAssociatePrice) : this()
        {
            _productAssociatePrice = productAssociatePrice;
            FillData(_productAssociatePrice);
            LoadAssociatePriceTypes();

            if (_productAssociatePrice.Id > 0)
                UserControlTitle = "ویرایش هزینه مرتبط با محصول";
            else
                UserControlTitle = "اضافه کردن هزینه مرتبط با محصول";
        }

        private void LoadAssociatePriceTypes()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var associatedPriceTypes = dbContext.ProductAssociatedPriceTypes.AsNoTracking()
                    .Select(item => new FmComboModel<int>
                    {
                        Title = item.Title,
                        Value = item.Id,
                    }).ToList();
                ProductAssociatePriceTypeComboBoxEdit.ItemsSource = associatedPriceTypes;
            }
        }

        private void ClickOnSave(object sender, RoutedEventArgs e)
        {
            ReadData(_productAssociatePrice);
            ApplicationDataStore.SendData("IsAddedOrChanged", true);
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void ClickOnCancel(object sender, RoutedEventArgs e)
        {
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void FillData(ProductAssociatePrice productAssociatePrice)
        {
            ProductAssociatePriceTypeComboBoxEdit.EditValue = productAssociatePrice.ProductAssociatedPriceTypeId;
            PriceSpinEdit.EditValue = productAssociatePrice.Price;
        }

        private void ReadData(ProductAssociatePrice productAssociatePrice)
        {
            productAssociatePrice.ProductAssociatedPriceTypeId = (int)ProductAssociatePriceTypeComboBoxEdit.EditValue;
            productAssociatePrice.Price = (double)PriceSpinEdit.EditValue;
        }

    }
}
