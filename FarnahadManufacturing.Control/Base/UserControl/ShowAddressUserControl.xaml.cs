using System.Windows;
using FarnahadManufacturing.Control.Base.Layout;

namespace FarnahadManufacturing.Control.Base.UserControl
{
    public partial class ShowAddressUserControl : FmGroupBox
    {
        public ShowAddressUserControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AddressDetailProperty = DependencyProperty.Register(
            "AddressDetail", typeof(string), typeof(ShowAddressUserControl), new PropertyMetadata(default(string), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var showAddressUserControl = (ShowAddressUserControl)d;
            if (e.NewValue is string newAddress)
            {

                //var provinceAndCityText = string.Empty;
                //if (newAddress.Province != null)
                //    provinceAndCityText += $"{newAddress.Province.Title} - ";
                //if (newAddress.City != null)
                //    provinceAndCityText += $"{newAddress.City.Title}";
                //provinceAndCityText = provinceAndCityText.Trim(" - ".ToCharArray());

                //showAddressUserControl.ProvinceAndCityLabel.Text = provinceAndCityText;
                showAddressUserControl.AddressDetailLabel.Text = showAddressUserControl.AddressDetail;
            }
        }

        public string AddressDetail
        {
            get => (string)GetValue(AddressDetailProperty);
            set => SetValue(AddressDetailProperty, value);
        }

        private void MapButtonOnClick(object sender, RoutedEventArgs e)
        {
            // TODO: show on map using latitude and longitude
        }
    }
}
