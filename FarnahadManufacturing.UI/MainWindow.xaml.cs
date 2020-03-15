using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.Layout;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.Window;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.UI.Common;
using FarnahadManufacturing.UI.UserControls.BaseConfiguration;
using FarnahadManufacturing.UI.UserControls.Configuration;

// CHECK
namespace FarnahadManufacturing.UI
{
    public partial class MainWindow : FmWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private System.Windows.Forms.Timer timer;
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Activate();
            UserNameBarButtonItem.Content = ApplicationSessionService.GetActiveUserName();
            LoginDateBarButtonItem.Content = $"زمان ورود: {DateTime.Now.ToShortTimeString()}";
            TodayDateStatusBarButtonItem.Content = DateTime.Now.ToShamsi();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 25000;
            timer.Tick += TimerOnElapsed;
            timer.Enabled = true;
        }

        private void TimerOnElapsed(object sender, EventArgs e)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var dbIsAlive = dbContext.Database.Exists();
                if (dbIsAlive)
                    ConnectionStatusBarButtonItem.Glyph =
                        ImageUtility.CreateSvgImage("Icons/ToolBar/connection_green_small.svg");
                else
                    ConnectionStatusBarButtonItem.Glyph =
                        ImageUtility.CreateSvgImage("Icons/ToolBar/connection_red_small.svg");
            }
        }

        private void SetToolBar(Dictionary<string, IBarItem> toolBarItems)
        {
            ToolBarControl.Items.Clear();
            foreach (var item in toolBarItems)
                ToolBarControl.Items.Add(item.Value);
        }

        private void PartOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcPart>("کالا ها");
        }

        private void ProductOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcProduct>("محصولات");
        }

        private void VendorOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcVendor>("فروشنده ها");
        }

        private void CategoryOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcCategory>("تقسیم بندی فعالیت ها");
        }

        private void LocationOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcLocation>("محل ها");
        }

        private void LocationGroupOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcLocationGroup>("گروه محل ها");
        }

        private void MyCompanyOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcMyCompany>("شرکت");
        }

        private void Carrier_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcCarrier>("پیک ها");
        }

        private void ProductCategoryOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowService.OpenUserControlDialog(new UcProductCategory());
        }

        private void TaxRateOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcTaxRate>("نرخ های مالیاتی");
        }

        private void UserOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcUser>("کاربران");
        }

        private void UserGroupOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcUserGroup>("گروه کاربران");
        }

        private void UomOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcUom>("واحد های اندازه گیری");
        }

        private void Country_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcCountry>("کشور ها");
        }

        private void City_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcCity>("شهر ها");
        }

        private void ProvinceOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcProvince>("استان ها");
        }

        private void TrackingOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcTracking>("ردیابی ها");
        }

        private void ShippingTermOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcShippingTerm>("شرایط تحویل کالا");
        }

        private void FobTypeOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcFobType>("فوب ها");
        }

        private void AddressTypeOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcAddressType>("نوع آدرس");
        }

        private void PaymentTermOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcPaymentTerm>("شرایط پرداخت");
        }

        private void ProductAssociatedPriceTypeOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcProductAssociatedPriceType>("نوع هزینه مرتبط با محصول ها");
        }

        private void OpenUserControlInNewTab<T>(string tabHeader) where T : UserControlPage
        {
            if (!UserControlIsAlreadyOpen<T>())
            {
                var userControl = (UserControlPage)Activator.CreateInstance<T>();
                var panel = new FmDocumentPanel();
                panel.TabCaption = tabHeader;
                panel.Content = userControl;
                if (!string.IsNullOrEmpty(userControl.ImagePath))
                    panel.CaptionImage = ImageUtility.CreateSvgImage(userControl.ImagePath);
                panel.IsVisibleChanged += PanelOnIsVisibleChanged;
                MyDocumentGroup.Add(panel);
                panel.IsActive = true;
            }
            else
            {
                ActivateTheUserControl<T>();
            }
        }

        private DocumentPanel _activeDocumentPanel;

        private void PanelOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.OldValue != (bool)e.NewValue && (bool)e.NewValue)
            {
                _activeDocumentPanel = sender as DocumentPanel;
                if (_activeDocumentPanel.Content is UserControlPage userControlBase)
                    SetToolBar(userControlBase.ToolBarItems);
            }
        }

        private bool UserControlIsAlreadyOpen<T>() where T : UserControlPage
        {
            foreach (var item in MyDocumentGroup.Items)
            {
                if (item is DocumentPanel documentPanel && documentPanel.Content is T)
                    return true;
            }
            return false;
        }

        private void ActivateTheUserControl<T>() where T : UserControlPage
        {
            foreach (var item in MyDocumentGroup.Items)
            {
                if (item is DocumentPanel documentPanel && documentPanel.Content is T)
                    documentPanel.IsActive = true;
            }
        }

        private void MainWindowOnClosing(object sender, CancelEventArgs e)
        {
            if (!MessageBoxService.AskForClose())
                e.Cancel = true;
        }
    }
}
