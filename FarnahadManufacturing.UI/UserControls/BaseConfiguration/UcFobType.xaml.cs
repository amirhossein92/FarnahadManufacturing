using System;
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
    public partial class UcFobType : FilterUserControlBase
    {
        private ObservableCollection<FobType> _fobTypes;
        private FobType _activeFobType;

        public UcFobType()
        {
            InitializeComponent();

            UserControlTitle = "فوب";
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
                var fobTypesQueryable = dbContext.FobTypes.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    fobTypesQueryable = fobTypesQueryable.Where(item => item.Title.Contains(title));
                var totalRecordsCount = fobTypesQueryable.Count();
                _fobTypes = fobTypesQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _fobTypes;
                PaginationUserControl.UpdateRecordsDetail(_fobTypes.Count, totalRecordsCount);
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activeFobType = new FobType();
            FillData(_activeFobType);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeFobType);
            if (_activeFobType.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeFobType).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

                IsEditing();
            }
            else
            {
                _activeFobType.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeFobType.CreatedDateTime = ApplicationSessionService.GetNowDateTime();

                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.FobTypes.Add(_activeFobType);
                    dbContext.SaveChanges();
                }

                OnAddToolBarItem();
            }

            MessageBoxService.SaveConfirmation(_activeFobType.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeFobType.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var fobType = dbContext.FobTypes.Find(_activeFobType.Id);
                    dbContext.FobTypes.Remove(fobType);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeFobType = new FobType();
                IsNotEditingAndAdding();
            }
        }

        protected override void OnAdding()
        {
            NameTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            NameTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeFobType.Title);
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
            if (searchGridControl.ItemsSource is ObservableCollection<FobType> fobTypes)
                EditData(fobTypes[rowIndex]);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var fobType = SearchGridControl.SelectedItem as FobType;
            EditData(fobType);
        }

        private void EditData(FobType fobType)
        {
            _activeFobType = fobType;
            FillData(_activeFobType);
            IsEditing();
        }

        private void FillData(FobType fobType)
        {
            NameTextEdit.Text = fobType.Title;
            DescriptionTextEdit.Text = fobType.Description;
        }

        private void ReadData(FobType fobType)
        {
            fobType.Title = NameTextEdit.Text;
            fobType.Description = DescriptionTextEdit.Text;
        }
    }
}
