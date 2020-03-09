using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Bars;
using FarnahadManufacturing.Control.Common;

// CHECK
namespace FarnahadManufacturing.Control.Base.ToolBar.Buttons
{
    public class FmAddBarButtonItem : BarButtonItem
    {
        public FmAddBarButtonItem()
        {
            Name = "Add";
            Content = "اضافه";
            Glyph = ImageUtility.CreateSvgImage("Icons/ToolBar/Add.svg");
        }
    }
}
