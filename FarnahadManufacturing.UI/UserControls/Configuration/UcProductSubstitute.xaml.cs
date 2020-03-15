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
    public partial class UcProductSubstitute : DialogUserControl
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
