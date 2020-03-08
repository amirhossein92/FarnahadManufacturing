﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.UI.Common;

namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcShippingTerm : FilterUserControlBase
    {
        private ObservableCollection<ShippingTerm> _shippingTerms;
        private ShippingTerm _activeShippingTerm;

        public UcShippingTerm()
        {
            InitializeComponent();

            UserControlTitle = "شرایط تحویل کالا";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchGridControl();
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text);
        }

        private void LoadSearchGridControlData(string title)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var shippingTermsQueryable = dbContext.ShippingTerms.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    shippingTermsQueryable = shippingTermsQueryable.Where(item => item.Title.Contains(title));
                var totalRecordsCount = shippingTermsQueryable.Count();
                _shippingTerms = shippingTermsQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _shippingTerms;
                PaginationUserControl.UpdateRecordsDetail(_shippingTerms.Count, totalRecordsCount);
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activeShippingTerm = new ShippingTerm();
            FillData(_activeShippingTerm);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeShippingTerm);
            if (_activeShippingTerm.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeShippingTerm).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            else
            {
                _activeShippingTerm.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeShippingTerm.CreatedDateTime = ApplicationSessionService.GetNowDateTime();

                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.ShippingTerms.Add(_activeShippingTerm);
                    dbContext.SaveChanges();
                }
            }

            MessageBoxService.SaveConfirmation(_activeShippingTerm.Title);
            LoadSearchGridControl();
            IsEditing();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeShippingTerm.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var shippingTerm = dbContext.ShippingTerms.Find(_activeShippingTerm.Id);
                    dbContext.ShippingTerms.Remove(shippingTerm);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeShippingTerm = new ShippingTerm();
                IsNotEditingAndAdding();
            }
        }

        protected override void OnAdding()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeShippingTerm.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInactiveHeaderTitle(UserControlTitle);
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void SearchGridControlOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var searchGridControl = (GridControl)sender;
            var rowIndex = searchGridControl.View.GetRowHandleByMouseEventArgs(e);
            if (rowIndex == DataControlBase.InvalidRowHandle)
                return;
            if (searchGridControl.ItemsSource is ObservableCollection<ShippingTerm> shippingTerms)
                EditData(shippingTerms[rowIndex]);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var shippingTerm = SearchGridControl.SelectedItem as ShippingTerm;
            EditData(shippingTerm);
        }

        private void EditData(ShippingTerm shippingTerm)
        {
            _activeShippingTerm = shippingTerm;
            FillData(_activeShippingTerm);
            IsEditing();
        }

        private void FillData(ShippingTerm shippingTerm)
        {
            NameTextEdit.Text = shippingTerm.Title;
            DescriptionTextEdit.Text = shippingTerm.Description;
        }

        private void ReadData(ShippingTerm shippingTerm)
        {
            shippingTerm.Title = NameTextEdit.Text;
            shippingTerm.Description = DescriptionTextEdit.Text;
        }
    }
}