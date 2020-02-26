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
using FarnahadManufacturing.Control.Base.UserControl;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcProductCategoryAddEdit : DialogUserControlBase
    {
        private int _productCategoryId;

        public UcProductCategoryAddEdit()
        {
            InitializeComponent();
        }

        public UcProductCategoryAddEdit(int productCategoryId) : this()
        {
            _productCategoryId = productCategoryId;
        }

        private void ClickOnSave(object sender, RoutedEventArgs e)
        {
        }

        private void ClickOnCancel(object sender, RoutedEventArgs e)
        {
        }
    }
}
