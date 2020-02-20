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
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Base.Dock;
using FarnahadManufacturing.UI.Base.Layout;

namespace FarnahadManufacturing.UI.Base.UserControl
{
    public partial class ShowAddressUserControl : FmGroupBox
    {
        public ShowAddressUserControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AddressProperty = DependencyProperty.Register(
            "Address", typeof(Address), typeof(ShowAddressUserControl), new PropertyMetadata(default(Address), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var showAddressUserControl = (ShowAddressUserControl)d;
            if (e.NewValue is Address newAddress)
            {
                var provinceAndCityText = string.Empty;
                if (newAddress.Province != null)
                    provinceAndCityText += $"{newAddress.Province.Title} - ";
                if (newAddress.City != null)
                    provinceAndCityText += $"{newAddress.City.Title}";
                provinceAndCityText = provinceAndCityText.Trim(" - ".ToCharArray());

                showAddressUserControl.ProvinceAndCityLabel.Text = provinceAndCityText;
                showAddressUserControl.AddressDetailLabel.Text = newAddress.AddressDetail;
            }
        }

        public Address Address
        {
            get => (Address)GetValue(AddressProperty);
            set => SetValue(AddressProperty, value);
        }

        private void MapButtonOnClick(object sender, RoutedEventArgs e)
        {
            // TODO: show on map using latitude and longitude
        }
    }
}
