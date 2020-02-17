using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf;
using DevExpress.Xpf.Bars;
using FarnahadManufacturing.UI.Common;

namespace FarnahadManufacturing.UI.Base.UserControl
{
    public abstract class FilterUserControlBase : UserControlBase
    {
        public FilterUserControlBase()
        {
            if (!this.IsInDesignMode())
                SetToolBarItems();
        }

        protected int CurrentPage = 1;
        protected int TotalRecordsCount;

        protected sealed override void SetToolBarItems()
        {
            var addButton = new BarButtonItem
            {
                Name = "Add",
                Content = "اضافه",
                Glyph = ImageUtility.CreateSvgImage("Icons/ToolBar/Add.svg"),
            };
            addButton.ItemClick += AddButtonOnToolBarItemClick;
            var saveButton = new BarButtonItem
            {
                Name = "Save",
                Content = "ذخیره",
                Glyph = ImageUtility.CreateSvgImage("Icons/ToolBar/Save.svg"),
            };
            saveButton.ItemClick += SaveButtonOnToolBarItemClick;
            var deleteButton = new BarButtonItem
            {
                Name = "Delete",
                Content = "حذف",
                Glyph = ImageUtility.CreateSvgImage("Icons/ToolBar/Delete.svg"),
            };
            deleteButton.ItemClick += DeleteButtonOnToolBarItemClick;
            ToolBarItems.Add(addButton.Name, addButton);
            ToolBarItems.Add(saveButton.Name, saveButton);
            ToolBarItems.Add(deleteButton.Name, deleteButton);
        }

        public abstract void LoadSearchGridControl();

        private void AddButtonOnToolBarItemClick(object sender, ItemClickEventArgs e)
        {
            OnAddToolBarItem();
        }

        protected abstract void OnAddToolBarItem();

        private void SaveButtonOnToolBarItemClick(object sender, ItemClickEventArgs e)
        {
            OnSaveToolBarItem();
        }

        protected abstract void OnSaveToolBarItem();

        private void DeleteButtonOnToolBarItemClick(object sender, ItemClickEventArgs e)
        {
            OnDeleteToolBarItem();
        }

        protected abstract void OnDeleteToolBarItem();


        protected void IsAdding()
        {
            ToolBarItems.ChangeToolBarItemStatus("Add", true);
            ToolBarItems.ChangeToolBarItemStatus("Save", true);
            ToolBarItems.ChangeToolBarItemStatus("Delete", false);
            OnAdding();
        }

        protected abstract void OnAdding();

        protected void IsEditing()
        {
            ToolBarItems.ChangeToolBarItemStatus("Add", true);
            ToolBarItems.ChangeToolBarItemStatus("Save", true);
            ToolBarItems.ChangeToolBarItemStatus("Delete", true);
            OnEditing();
        }

        protected abstract void OnEditing();

        protected void IsNotEditingAndAdding()
        {
            ToolBarItems.ChangeToolBarItemStatus("Add", true);
            ToolBarItems.ChangeToolBarItemStatus("Save", false);
            ToolBarItems.ChangeToolBarItemStatus("Delete", false);
            OnNotEditingAndAdding();
        }

        protected abstract void OnNotEditingAndAdding();
    }
}
