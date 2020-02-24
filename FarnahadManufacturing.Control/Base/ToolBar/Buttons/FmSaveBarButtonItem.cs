using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Bars;
using FarnahadManufacturing.Control.Common;

namespace FarnahadManufacturing.Control.Base.ToolBar.Buttons
{
    public class FmSaveBarButtonItem : BarButtonItem
    {
        public FmSaveBarButtonItem()
        {
            Name = "Save";
            Content = "ذخیره";
            Glyph = ImageUtility.CreateSvgImage("Icons/ToolBar/Save.svg");
        }
    }
}
