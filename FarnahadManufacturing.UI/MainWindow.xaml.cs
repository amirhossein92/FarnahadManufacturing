﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using FarnahadManufacturing.UI.Base.UserControl;
using FarnahadManufacturing.UI.Common;
using FarnahadManufacturing.UI.UserControls;
using FarnahadManufacturing.UI.UserControls.BaseConfiguration;
using FarnahadManufacturing.UI.UserControls.Configuration;
using MessageBox = System.Windows.Forms.MessageBox;

namespace FarnahadManufacturing.UI
{
    public partial class MainWindow : DXWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void SetToolBar(Dictionary<string, IBarItem> toolBarItems)
        {
            ToolBarControl.Items.Clear();
            foreach (var item in toolBarItems)
                ToolBarControl.Items.Add(item.Value);
        }

        private void Country_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcCountry>("کشور ها");
        }

        private void City_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcCity>("شهر ها");
        }

        private void Carrier_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcCarrier>("پیک ها");
        }

        private void UserOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcUser>("کاربران");
        }

        private void VendorOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcVendor>("فروشنده ها");
        }

        private void OpenUserControlInNewTab<T>(string tabHeader) where T : UserControlBase
        {
            if (!UserControlIsAlreadyOpen<T>())
            {
                var panel = new DocumentPanel();
                panel.TabCaption = tabHeader;
                panel.AllowClose = true;
                panel.ShowCloseButton = true;
                panel.Content = Activator.CreateInstance<T>();
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
                if (_activeDocumentPanel.Content is UserControlBase userControlBase)
                    SetToolBar(userControlBase.ToolBarItems);
            }
        }

        private bool UserControlIsAlreadyOpen<T>() where T : UserControlBase
        {
            foreach (var item in MyDocumentGroup.Items)
            {
                if (item is DocumentPanel documentPanel && documentPanel.Content is T)
                    return true;
            }
            return false;
        }

        private void ActivateTheUserControl<T>() where T : UserControlBase
        {
            foreach (var item in MyDocumentGroup.Items)
            {
                if (item is DocumentPanel documentPanel && documentPanel.Content is T)
                    documentPanel.IsActive = true;
            }
        }
    }
}
