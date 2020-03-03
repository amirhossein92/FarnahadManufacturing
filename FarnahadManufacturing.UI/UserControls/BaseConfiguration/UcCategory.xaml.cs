using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Control.Base.ToolBar.Buttons;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcCategory : UserControlBase
    {
        private ObservableCollection<Category> _categories;

        public UcCategory()
        {
            InitializeComponent();

            UserControlTitle = "تقسیم بندی فعالیت ها";
            SetToolBarItems();
            InitialData();
        }

        protected sealed override void SetToolBarItems()
        {
            var addButton = new FmAddBarButtonItem();
            addButton.ItemClick += AddButtonOnToolBarItemClick;
            var editButton = new FmEditBarButtonItem();
            editButton.ItemClick += EditButtonOnToolBarItemClick;
            var deleteButton = new FmDeleteBarButtonItem();
            deleteButton.ItemClick += DeleteButtonOnToolBarItemClick;
            ToolBarItems.Add(addButton.Name, addButton);
            ToolBarItems.Add(editButton.Name, editButton);
            ToolBarItems.Add(deleteButton.Name, deleteButton);

            ToolBarItems.ChangeToolBarItemStatus("Edit", false);
            ToolBarItems.ChangeToolBarItemStatus("Delete", false);
        }

        private void AddButtonOnToolBarItemClick(object sender, ItemClickEventArgs e)
        {
            WindowService.OpenUserControlDialog(new UcCategoryAddEdit());
            LoadCategories();
            LoadCategoriesTreeList();
        }

        private void EditButtonOnToolBarItemClick(object sender, ItemClickEventArgs e)
        {
            if (CategoryTreeListControl.SelectedItem is Category category)
            {
                WindowService.OpenUserControlDialog(new UcCategoryAddEdit(category.Id));
                LoadCategories();
                LoadCategoriesTreeList();
            }
            else
            {
                // No Valid Item Selected

            }
        }

        private void DeleteButtonOnToolBarItemClick(object sender, ItemClickEventArgs e)
        {
            if (CategoryTreeListControl.SelectedItem is Category category)
            {
                if (MessageBoxService.AskForDelete(category.Title) == true)
                {
                    using (var dbContext = new FarnahadManufacturingDbContext())
                    {
                        var categoryInDb = dbContext.Categories.First(item => item.Id == category.Id);
                        if (dbContext.Categories.Any(item => item.ParentCategoryId == categoryInDb.Id))
                        {
                            MessageBoxService.CannotDeleteParent(categoryInDb.Title);
                        }
                        else
                        {
                            dbContext.Categories.Remove(categoryInDb);
                            dbContext.SaveChanges();
                            LoadCategories();
                            LoadCategoriesTreeList();
                        }
                    }
                }
            }
            else
            {
                // No Valid Item Selected

            }
        }

        protected sealed override void InitialData()
        {
            LoadCategories();
            LoadCategoriesTreeList();
        }

        private void LoadCategories()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var categories = dbContext.Categories.AsNoTracking().ToList();
                _categories = new ObservableCollection<Category>(categories);
            }
        }

        private void LoadCategoriesTreeList()
        {
            CategoryTreeListView.Nodes.Clear();
            foreach (var category in _categories.Where(item => item.ParentCategoryId == null))
                CategoryTreeListView.Nodes.Add(GetChildrenCategory(category));
        }

        private TreeListNode GetChildrenCategory(Category parentCategory)
        {
            var treeList = new TreeListNode(parentCategory);
            var children = _categories.Where(item => item.ParentCategoryId == parentCategory.Id);
            foreach (var child in children)
                treeList.Nodes.Add(GetChildrenCategory(child));

            return treeList;
        }

        private void CategoryTreeListControl_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.NewItem is Category category)
            {
                ToolBarItems.ChangeToolBarItemStatus("Edit", true);
                ToolBarItems.ChangeToolBarItemStatus("Delete", true);
            }
            else
            {
                ToolBarItems.ChangeToolBarItemStatus("Edit", false);
                ToolBarItems.ChangeToolBarItemStatus("Delete", false);
            }
        }
    }
}
