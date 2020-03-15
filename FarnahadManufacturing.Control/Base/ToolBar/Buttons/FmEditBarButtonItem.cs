using DevExpress.Xpf.Bars;
using FarnahadManufacturing.Control.Common;

namespace FarnahadManufacturing.Control.Base.ToolBar.Buttons
{
    public class FmEditBarButtonItem : BarButtonItem
    {
        public FmEditBarButtonItem()
        {
            Name = "Edit";
            Content = "ویرایش";
            Glyph = ImageUtility.CreateSvgImage("Icons/ToolBar/Edit.svg");
        }
    }
}
