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
    public partial class UcProductSubstitute : DialogUserControlBase
    {
        private ProductSubstitute _productSubstitute;

        private UcProductSubstitute()
        {
            InitializeComponent();
        }

        public UcProductSubstitute(ProductSubstitute productSubstitute) : this()
        {
            _productSubstitute = productSubstitute;
            FillData(_productSubstitute);

            if (_productSubstitute.Id > 0)
                UserControlTitle = "ویرایش محصول جایگزین";
            else
                UserControlTitle = "اضافه کردن محصول جایگزین";

            LoadSubstituteProducts();
        }

        private void LoadSubstituteProducts()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var productsQueryable = dbContext.Products.AsQueryable();
                if (_productSubstitute.ProductId > 0)
                    productsQueryable =
                        productsQueryable.Where(item => item.Id != _productSubstitute.ProductId);
                var products = productsQueryable
                    .Select(item => new FmComboModel<int> { Title = item.Title, Value = item.Id })
                    .ToList();
                ProductSubstituteComboBoxEdit.ItemsSource = products;
            }
        }

        private void ClickOnSave(object sender, RoutedEventArgs e)
        {
            ReadData(_productSubstitute);
            ApplicationDataStore.SendData("IsAddedOrChanged", true);
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void ClickOnCancel(object sender, RoutedEventArgs e)
        {
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void FillData(ProductSubstitute productSubstitute)
        {
            ProductSubstituteComboBoxEdit.EditValue = productSubstitute.SubstituteProductId;
            NoteTextEdit.Text = productSubstitute.Note;
        }

        private void ReadData(ProductSubstitute productSubstitute)
        {
            productSubstitute.SubstituteProductId = (int)ProductSubstituteComboBoxEdit.EditValue;
            productSubstitute.Note = NoteTextEdit.Text;
        }
    }
}
