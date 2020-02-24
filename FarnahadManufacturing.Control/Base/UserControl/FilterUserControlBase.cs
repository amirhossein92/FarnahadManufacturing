﻿using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Bars;
using FarnahadManufacturing.Control.Base.ToolBar.Buttons;
using FarnahadManufacturing.Control.Common;

namespace FarnahadManufacturing.Control.Base.UserControl
{
    public abstract class FilterUserControlBase : UserControlBase
    {
        public FilterUserControlBase()
        {
            if (!this.IsInDesignMode())
                SetToolBarItems();
        }

        protected sealed override void SetToolBarItems()
        {
            var addButton = new FmAddBarButtonItem();
            addButton.ItemClick += AddButtonOnToolBarItemClick;
            var saveButton = new FmSaveBarButtonItem();
            saveButton.ItemClick += SaveButtonOnToolBarItemClick;
            var deleteButton = new FmDeleteBarButtonItem();
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
