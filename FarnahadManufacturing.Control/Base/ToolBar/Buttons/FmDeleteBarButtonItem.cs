﻿using DevExpress.Xpf.Bars;
using FarnahadManufacturing.Control.Common;

namespace FarnahadManufacturing.Control.Base.ToolBar.Buttons
{
    public class FmDeleteBarButtonItem : BarButtonItem
    {
        public FmDeleteBarButtonItem()
        {
            Name = "Delete";
            Content = "حذف";
            Glyph = ImageUtility.CreateSvgImage("Icons/ToolBar/Delete.svg");
        }
    }
}
