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
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Common;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcUom : FilterUserControlBase
    {
        private ObservableCollection<Uom> _uoms;
        private Uom _activeUom;

        public UcUom()
        {
            InitializeComponent();

            UserControlTitle = "واحد اندازه گیری";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchGridControl();

            LoadUomTypeComboBox();
        }

        private void LoadUomTypeComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var uomTypes = dbContext.UomTypes.Select(item => new FmComboModel<int>
                {
                    Value = item.Id,
                    Title = item.Title
                }).ToList();
                UomTypeComboBoxEdit.ItemsSource = uomTypes;
            }
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchAbbreviationTitleTextEdit.Text);
        }

        private void LoadSearchGridControlData(string title, string abbreviation)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var uomQueryable = dbContext.Uoms.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    uomQueryable = uomQueryable.Where(item => item.Title.Contains(title));
                if (!string.IsNullOrEmpty(abbreviation))
                    uomQueryable = uomQueryable.Where(item => item.Abbreviation.Contains(abbreviation));
                var totalRecordsCount = uomQueryable.Count();
                _uoms = uomQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _uoms;
                PaginationUserControl.UpdateRecordsDetail(_uoms.Count, totalRecordsCount);
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activeUom = new Uom();
            FillData(_activeUom);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeUom);
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                if (_activeUom.Id > 0 && _activeUom.ReadOnly)
                {
                    MessageBoxService.CannotEditPrompt(_activeUom.Title);
                }
                else
                {
                    if (_activeUom.Id > 0)
                    {
                        dbContext.Entry(_activeUom).State = EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        _activeUom.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                        _activeUom.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                        dbContext.Uoms.Add(_activeUom);
                        dbContext.SaveChanges();
                    }

                    MessageBoxService.SaveConfirmation(_activeUom.Title);
                    LoadSearchGridControl();
                    IsAdding();
                }
            }
        }

        protected override void OnDeleteToolBarItem()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var uomInDb = dbContext.Uoms.Find(_activeUom.Id);
                dbContext.Uoms.Remove(uomInDb);
                dbContext.SaveChanges();
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
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeUom.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInactiveHeaderTitle(UserControlTitle);
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            PaginationUserControl.CurrentPage = 1;
            LoadSearchGridControl();
        }

        private void SearchGridControlOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var searchGridControl = (GridControl)sender;
            var rowIndex = searchGridControl.View.GetRowHandleByMouseEventArgs(e);
            if (rowIndex == DataControlBase.InvalidRowHandle)
                return;
            if (searchGridControl.ItemsSource is ObservableCollection<Uom> uoms)
                EditData(uoms[rowIndex]);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var uom = SearchGridControl.SelectedItem as Uom;
            EditData(uom);
        }

        private void EditData(Uom uom)
        {
            _activeUom = uom;
            FillData(_activeUom);
            IsEditing();
        }

        private void FillData(Uom uom)
        {
            AbbreviationTextEdit.Text = uom.Abbreviation;
            TitleTextEdit.Text = uom.Title;
            DescriptionTextEdit.Text = uom.Description;
            UomTypeComboBoxEdit.EditValue = uom.UomTypeId;
            ConversionSpinEdit.EditValue = uom.Conversion;
            IsActiveCheckEdit.EditValue = uom.IsActive;
            ReadOnlyCheckEdit.EditValue = uom.ReadOnly;
        }

        private void ReadData(Uom uom)
        {
            uom.Abbreviation = AbbreviationTextEdit.Text;
            uom.Title = TitleTextEdit.Text;
            uom.Description = DescriptionTextEdit.Text;
            uom.UomTypeId = Convert.ToInt32(UomTypeComboBoxEdit.EditValue);
            uom.Conversion = Convert.ToDouble(ConversionSpinEdit.EditValue);
            uom.IsActive = (bool)IsActiveCheckEdit.EditValue;
            uom.ReadOnly = (bool)ReadOnlyCheckEdit.EditValue;
        }
    }
}
